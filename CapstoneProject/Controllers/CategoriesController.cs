using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    [Authorize(Roles ="Admin,Staff")]
    public class CategoriesController : Controller
    {
        private readonly CapstoneProjectDbContext _db;
        private readonly ILogger<UserController> _logger;

        // Inject DB context and logger
        public CategoriesController(CapstoneProjectDbContext db, ILogger<UserController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // List all categories
        public IActionResult Index()
        {
            return View(_db.ItemCategories.ToList());
        }

        // Show add category form
        public IActionResult Add()
        {
            return View();
        }

        // Add a new category
        [HttpPost]
        public IActionResult Add(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("Posted category was null or the name only contained whitespace characters");

                // Check for duplicate (case-insensitive)
                if (_db.ItemCategories.Select(x => x.Name.ToUpper()).Contains(name.ToUpper()))
                    throw new Exception($"The category {name} already exists");

                _db.ItemCategories.Add(new ItemCategory() { Name = name });
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}\t{ex.Message}\n");
                return BadRequest();
            }
        }

        // Show edit category form
        public IActionResult Edit(int id)
        {
            return View(_db.ItemCategories.Find(id));
        }

        // Edit a category
        [HttpPost]
        public IActionResult Edit([FromBody] ItemCategory category)
        {
            try
            {
                if (category == null || string.IsNullOrEmpty(category.Name))
                    throw new Exception("The category or its name was null or empty");
                if (!ModelState.IsValid)
                    throw new Exception("Invalid model state");
                if (_db.ItemCategories.Select(x => x.Name.ToUpper()).Contains(category.Name.ToUpper()))
                    throw new Exception($"The category {category.Name} already exists");

                _db.ItemCategories.Update(category);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}\t{ex.Message}\n");
                return BadRequest();
            }
        }
    }
}