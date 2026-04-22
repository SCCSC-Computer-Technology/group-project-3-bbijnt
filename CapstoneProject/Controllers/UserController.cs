using CapstoneProject.Data;
using CapstoneProject.Models;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapstoneProject.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using CapstoneProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CapstoneProject.Controllers
{

    [Authorize(Roles = "Admin, Staff, Student, Volunteer")]
    public class UserController : Controller
    {
        private readonly CapstoneProjectDbContext _db;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<CapstoneProjectUser> _userManager;

        // Inject DB context, logger, and user manager
        public UserController(CapstoneProjectDbContext db, ILogger<UserController> logger, UserManager<CapstoneProjectUser> userManager)
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
        }


        // Show find user form (admin/staff only)
        [Authorize(Roles = "Admin,Staff")]
        public IActionResult FindUser()
        {
            return View();
        }

        // Handle find user form submission
        [HttpPost, ActionName("FindUser")]
        [Authorize(Roles = "Admin,Staff")]
        public IActionResult FindUserPost(string userId)
        {
            var user = _db.Users.SingleOrDefault(x => x.StudentId == userId);
            if (user != null)
            {
                string username = user.FirstName + " " + user.LastName;
                return RedirectToAction("AidForm", new { username = username, userID = userId });
            }
            return NotFound();
        }


        // Show aid form for a user (student or found by admin/staff)
        public async Task<IActionResult> AidForm(string username, string userID)
        {
            CapstoneProjectUser? user;
            if (string.IsNullOrWhiteSpace(userID))
            {
                user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    bool isStudent = await _userManager.IsInRoleAsync(user, "student");
                    if (!isStudent)
                        return View("FindUser");
                }
            }
            else
            {
                user = _db.Users.SingleOrDefault(x => x.StudentId == userID);
            }
            if (user == null)
                return BadRequest();

            if (user.IsRegistrationComplete)
            {
                ViewBag.Username = $"{user.FirstName} {user.LastName}";
                ViewBag.UserID = user.StudentId;
                ViewBag.Points = user.Points;
                return View();
            }
            else
            {
                return RedirectToAction("WelcomePageView", "Registration");
            }
        }


        // Populate item dropdown for a subcategory
        public JsonResult PopulateItemDropdown(int subcategoryID)
        {
            return Json(_db.Items.Where(x => x.SubcategoryID == subcategoryID).ToList());
        }




        // Populate subcategory dropdown for a category
        public JsonResult PopulateSubcategoryDropdown(int category)
        {
            return Json(_db.ItemSubcategories.Where(x => x.CategoryID == category).ToList());
        }


        // Populate category dropdown
        public JsonResult PopulateCategoryDropdown()
        {
            return Json(_db.ItemCategories.ToList());
        }


        // Get item by ItemID
        public JsonResult GetItemByItemID(int itemID)
        {
            return Json(_db.Items.Where(u => u.ItemID == itemID));
        }



        // POST: /User/AidForm (handles form submission)
        [HttpPost]
        public IActionResult AidForm()
        {
            TempData["success"] = "Transaction created successfully";
            return RedirectToAction("Index", "Home");
        }


        // POST: /User/PostTransaction (handles cart/transaction submission)
        [HttpPost]
        public ActionResult PostTransaction([FromBody] CartList cartList)
        {
            if (cartList == null)
            {
                _logger.LogError("cartList is null in PostTransaction");
                return BadRequest(new { error = "cartList is null" });
            }

            var user = _db.Users.SingleOrDefault(x => x.StudentId == cartList.UserID);
            if (user == null)
            {
                _logger.LogError("User not found for StudentId: {StudentId}", cartList.UserID);
                return BadRequest(new { error = "User not found" });
            }

            var transactionobj = new Transaction
            {
                UserID = user.StudentId,
                SpecialRequests = cartList.SpecialRequests?.Trim() ?? string.Empty,
                AppointmentDateTime = cartList.AppointmentDateTime
            };

            if (!ModelState.IsValid)
            {
                LogModelStateErrors();
                var errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .Select(x => new { Field = x.Key, Errors = x.Value != null ? x.Value.Errors.Select(e => e.ErrorMessage).ToList() : new List<string>() })
                    .ToList();
                _logger.LogError("ModelState invalid in PostTransaction: {@Errors}", errors);
                return BadRequest(new { error = "ModelState invalid", details = errors });
            }

            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.Transactions.Add(transactionobj);
                    _db.SaveChanges();

                    foreach (var cartItem in cartList.cartList)
                    {
                        var line = new TransactionLineItem()
                        {
                            TransactionID = transactionobj.TransactionID,
                            ItemID = cartItem.Item.ItemID,
                            Quantity = cartItem.Info.QuantityReq,
                            IsPAL = cartItem.Info.IsPal,
                            IsRG = cartItem.Info.IsRG,
                        };
                        _db.TransactionLineItems.Add(line);
                    }

                    _db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    _logger.LogError(ex, "Unable to complete transaction");
                    TempData["success"] = "Failed to create item";
                    return StatusCode(500, new { error = "Exception occurred", details = ex.Message });
                }
            }
            TempData["success"] = "Item created successfully";
            return Ok();
        }

        // Log all model state errors for diagnostics
        private void LogModelStateErrors()
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogError("Validation error in field '{Field}': {ErrorMessage}", state.Key, error.ErrorMessage);
                }
            }
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyRequests()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var requests = _db.Transactions
                .Where(t => t.UserID == user.StudentId)
                .Select(t => new CapstoneProject.ViewModels.MyRequestViewModel
                {
                    TransactionID = t.TransactionID,
                    Date = t.Date,
                    Status = t.IsProcessed ? "Processed" : "Pending",
                    SpecialRequests = t.SpecialRequests,
                    TotalCost = t.LineItems.Sum(li => li.Item.PointCost * (int)li.Quantity) + t.AdditionalPointCost
                })
                .OrderByDescending(t => t.TransactionID)
                .ToList();

            return View(requests);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> RequestDetails(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var transaction = _db.Transactions
                .Where(t => t.TransactionID == id && t.UserID == user.StudentId)
                .Select(t => new CapstoneProject.ViewModels.MyRequestDetailsViewModel
                {
                    TransactionID = t.TransactionID,
                    Date = t.Date,
                    Status = t.IsProcessed ? "Processed" : "Pending",
                    SpecialRequests = t.SpecialRequests,
                    AdditionalPointCost = t.AdditionalPointCost,
                    TotalCost = t.LineItems.Sum(li => li.Item.PointCost * (int)li.Quantity) + t.AdditionalPointCost,
                    Items = t.LineItems.Select(li => new CapstoneProject.ViewModels.MyRequestItemViewModel
                    {
                        ItemID = li.ItemID,
                        Description = li.Item.Description,
                        Quantity = li.Quantity,
                        PointCost = li.Item.PointCost,
                        IsRG = li.IsRG,
                        IsPAL = li.IsPAL
                    }).ToList()
                })
                .FirstOrDefault();

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

    }
}

    //[HttpPost]
    //public int PostTransaction([FromBody] TransactionDTO transaction)
    //{

    //    //String user = transaction.UserID;

    //    //var date = DateOnly.FromDateTime(DateTime.Now);

    //    //Transaction transactionobj = new Transaction() { UserID = user, Date = date };

    //    //if (ModelState.IsValid)
    //    //{
    //    //    _db.Transactions.Add(transactionobj);
    //    //    _db.SaveChanges();
    //    //    TempData["success"] = "Item created successfully";

    //    //    return ;
    //    //}
    //    //return View();
    //}
