using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneProject.Services;
using System.Drawing;

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
            var items = _db.Items
                .Include(i => i.ItemSubcategory)
                .ToList();




            return View(items);
        }

        public ActionResult GetBarcodeImage(int? id)
        {
            if (id == null)
                return NotFound();

            var item = _db.Items.Find(id);

            if (item == null)
                return NotFound();


            var barcodeText = BarcodeHelper.FormatUUIDForBarcode(item.UUID);
            Bitmap barcodeImage = BarcodeHelper.CreateBarcodeImage(barcodeText);



            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return File(ms.ToArray(), "image/png");
            }
        }

        // Show create item form
        [HttpGet]
        [HttpGet]
        public IActionResult Create()
        {
            var item = new Item
            {
                UUID = "",
                Description = "",
                SubcategoryID = 0,
                Quantity = 0,
                PointCost = 0
            };

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            _db.Items.Add(obj);
            _db.SaveChanges();

            TempData["success"] = "Item created successfully";
            return RedirectToAction(nameof(Index));
        }



        // Show edit item form
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var item = _db.Items
                .Include(i => i.ItemSubcategory)
                .FirstOrDefault(i => i.ItemID == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // Edit an item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            _db.Items.Update(obj);
            _db.SaveChanges();

            TempData["success"] = "Item updated successfully";
            return RedirectToAction(nameof(Index));
        }

        // Show delete item confirmation
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var item = _db.Items
                .Include(i => i.ItemSubcategory)
                .FirstOrDefault(i => i.ItemID == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // Delete an item
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null)
                return NotFound();

            var item = _db.Items.Find(id);
            if (item == null)
                return NotFound();

            _db.Items.Remove(item);
            _db.SaveChanges();

            TempData["success"] = "Item deleted successfully";
            return RedirectToAction(nameof(Index));
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
