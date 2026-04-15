using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InventoryController : Controller
    {
        private readonly CapstoneProjectDbContext _db;

        // Inject DB context
        public InventoryController(CapstoneProjectDbContext db)
        {
            _db = db;
        }

        // List all inventory items
        public IActionResult Index()
        {
            var objInventoryList = _db.Items
                .Include(i => i.ItemSubcategory)
                .ToList();
            return View(objInventoryList);
        }

        // Show create item form
        public IActionResult Create(Item obj)
        {
            return View(obj);
        }

        // Create a new item
        [HttpPost, ActionName("Create")]
        public IActionResult CreatePOST([FromBody] Item obj)
        {
            if (!ModelState.IsValid)
                return BadRequest("ModelState invalid");
            try
            {
                _db.Items.Add(obj);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // Show edit item form
        public IActionResult Edit(Item obj)
        {
            var objFromDb = _db.Items.Where(x => x.ItemID == obj.ItemID).Include(x => x.ItemSubcategory).SingleOrDefault();
            if (objFromDb == null)
                return NotFound();
            return View(objFromDb);
        }

        // Edit an item
        [HttpPost, ActionName("Edit")]
        public IActionResult EditPOST([FromBody] Item obj)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _db.Items.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Item updated successfully";
            return Ok();
        }

        // Show delete item confirmation
        public IActionResult Delete(string? itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                return NotFound();
            if (!int.TryParse(itemId, out int id))
                return BadRequest();
            var itemFromDb = _db.Items.Find(id);
            if (itemFromDb == null)
                return NotFound();
            // Get subcategory name
            var subcategory = _db.ItemSubcategories.FirstOrDefault(sc => sc.SubcategoryID == itemFromDb.SubcategoryID);
            ViewBag.SubcategoryName = subcategory != null ? subcategory.Name : "Unknown";
            return View(itemFromDb);
        }

        // Delete an item
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(string? itemId)
        {
            if (string.IsNullOrEmpty(itemId) || !int.TryParse(itemId, out int id))
                return BadRequest();
            var obj = _db.Items.Find(id);
            if (obj == null)
                return NotFound();
            _db.Items.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Item deleted successfully";
            return RedirectToAction("LookUp", "Inventory");
        }

        // List all inventory items for lookup
        public IActionResult LookUp()
        {
            var objInventoryList = _db.Items
                .Include(i => i.ItemSubcategory)
                .ToList();
            ViewBag.InventoryList = objInventoryList;
            return View();
        }

        // Lookup item by UUID and redirect to edit or create
        [HttpPost, ActionName("LookUp")]
        public IActionResult LookUpPost(Item obj)
        {
            if (_db.Items.Any(item => item.UUID == obj.UUID))
            {
                var existingItem = _db.Items.FirstOrDefault(item => item.UUID == obj.UUID);
                if (existingItem != null)
                    return RedirectToAction("Edit", existingItem);
                else
                    return NotFound();
            }
            else
            {
                return RedirectToAction("Create", obj);
            }
        }
    }
}
