using CapstoneProject.Areas.Identity.Data;
using CapstoneProject.Data;
using CapstoneProject.Models;
using CapstoneProject.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

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

            var transactionobj = new Transaction { UserID = user.StudentId, SpecialRequests = cartList.SpecialRequests?.Trim() ?? string.Empty };

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

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(string Name, string Email, string Message)
        {
            try
            {
                var fromEmail = "sportsapplication206@gmail.com";
                var password = "tvbm affx ignj gbfu";

                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(fromEmail, password);
                    client.EnableSsl = true;

                    MailMessage adminMail = new MailMessage();
                    adminMail.To.Add(fromEmail);
                    adminMail.From = new MailAddress(fromEmail, "Chaser's Pantry", Encoding.UTF8);
                    adminMail.Subject = "New Contact Form Message";

                    adminMail.Body = $@"
                    <div style='font-family: Arial;'>
                    <h2>New Contact Form Submission</h2>
                    <p><b>Name:</b> {Name}</p>
                    <p><b>Email:</b> {Email}</p>
                    <p><b>Message:</b><br>{Message}</p>
                    </div>";
                    adminMail.IsBodyHtml = true;

                    await client.SendMailAsync(adminMail);

                    MailMessage userMail = new MailMessage();
                    userMail.To.Add(Email);
                    userMail.From = new MailAddress(fromEmail, "Chaser's Pantry", Encoding.UTF8);
                    userMail.Subject = "We Received Your Message";

                    userMail.Body = $@"
                    <div style='font-family: Arial, sans-serif; background-color:#f4f4f4; padding:20px;'>
                    <div style='max-width:600px; margin:auto; background:white; padding:30px; border-radius:10px;'>


                    <h2 style='color:#004e8c; text-align:center;'>Thank You, {Name}!</h2>

                    <p style='color:#333; font-size:16px;'>
                        We’ve received your message and truly appreciate you reaching out.
                    </p>

                    <p style='color:#333; font-size:16px;'>
                        Our team will review your message and get back to you as soon as possible.
                    </p>

                    <div style='background:#f9f9f9; padding:15px; border-radius:8px; margin-top:20px;'>
                        <p><b>Your Message:</b></p>
                        <p style='color:#555;'>{Message}</p>
                    </div>

                    <p style='margin-top:25px; color:#333;'>
                        If you have any additional questions, feel free to reply to this email.
                    </p>

                    <p style='margin-top:30px; font-weight:bold; color:#004e8c;'>
                        – Chaser's Pantry
                    </p>

                    </div>
                    </div>";

                    userMail.IsBodyHtml = true;

                    await client.SendMailAsync(userMail);
                }

                ViewBag.SuccessMessage = "Your message has been sent successfully.";
            }
            catch
            {
                ViewBag.SuccessMessage = "There was a problem sending your message.";
            }

            return View();
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
