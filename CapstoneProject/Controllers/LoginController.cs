using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CapstoneProject.Data;
using CapstoneProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CapstoneProject.Areas.Identity.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CapstoneProject.Controllers
{
	public class LoginController : Controller
	{
		private readonly CapstoneProjectDbContext _db;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<CapstoneProjectUser> _userManager;
		private readonly SignInManager<CapstoneProjectUser> _signInManager;
		private readonly ILogger<LoginController> _logger;
		private readonly IConfiguration _config;

		// Inject configuration for demo user IDs
		public LoginController(CapstoneProjectDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<CapstoneProjectUser> userManager, SignInManager<CapstoneProjectUser> signInManager, ILogger<LoginController> logger, IConfiguration config)
		{
			_db = db;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_config = config;
		}

		// GET: /Login
		// Shows the login page, or redirects if already authenticated
		[HttpGet]
		public IActionResult Login()
		{
			try
			{
				if (User?.Identity != null && User.Identity.IsAuthenticated)
					return RedirectToAction("Index", "Home");
				return View();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error loading login page.");
				ViewBag.ErrorMessage = "An unexpected error occurred. Please try again later.";
				return View();
			}
		}

		// POST: /Login
		// Handles staff login form submission
		[HttpPost]
		public async Task<IActionResult> Login(StaffSignIn obj)
		{
			if (!ModelState.IsValid)
				return View();
			try
			{
				if (obj == null || string.IsNullOrWhiteSpace(obj.UserID) || string.IsNullOrWhiteSpace(obj.Password))
				{
					ViewBag.ErrorMessage = "User ID and Password are required.";
					return View();
				}

				var userFromDb = await _db.StaffRegisters.FirstOrDefaultAsync(u => u.UserID == obj.UserID);
				if (userFromDb == null || userFromDb.Password != obj.Password)
				{
					ViewBag.ErrorMessage = "Invalid User ID or Password.";
					return View();
				}

				// Build claims for authentication
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, userFromDb.UserID ?? string.Empty),
					new Claim(ClaimTypes.Name, userFromDb.UserID ?? string.Empty)
				};

				var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
				var authProperties = new AuthenticationProperties { IsPersistent = false };

				if (_httpContextAccessor.HttpContext != null)
					await _httpContextAccessor.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

				// Redirect to returnUrl if present, else home
				var returnUrl = HttpContext.Request.Query["ReturnUrl"].FirstOrDefault();
				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					return Redirect(returnUrl);

				_logger.LogInformation($"User {userFromDb.UserID} logged in successfully.");
				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Login failed for user {UserID}", obj?.UserID);
				ViewBag.ErrorMessage = "An unexpected error occurred during login. Please try again later.";
				return View();
			}
		}

		public async Task<IActionResult> Logout()
		{
			try
			{
				if (_httpContextAccessor.HttpContext != null)
				{
					await _httpContextAccessor.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
				}
				_logger.LogInformation("User logged out successfully.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Logout failed.");
			}
			return RedirectToAction("Login");
		}

		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SignUp(StaffRegister obj)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			try
			{
				if (obj == null || string.IsNullOrWhiteSpace(obj.UserID))
				{
					ViewBag.ErrorMessage = "User ID is required.";
					return View();
				}
				var existingUser = _db.StaffRegisters.FirstOrDefault(u => u.UserID == obj.UserID);
				if (existingUser != null)
				{
					ViewBag.ErrorMessage = "Account already registered.";
					return View();
				}

				_db.StaffRegisters.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "User created successfully.";
				_logger.LogInformation($"User {obj.UserID} registered successfully.");
				return RedirectToAction("Login");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Sign up failed for user {UserID}", obj?.UserID);
				ViewBag.ErrorMessage = "An unexpected error occurred during sign up. Please try again later.";
				return View();
			}
		}

		[HttpPost]
		// POST: /Login/DemoSccLogin
		// Handles demo SSO login using IDs from appsettings.json
		[HttpPost]
		public async Task<IActionResult> DemoSccLogin(string userType)
		{
			try
			{
				// Get demo user IDs from config
				string? adminId = _config["DemoUsers:AdminId"];
				string? studentId = _config["DemoUsers:StudentId"];
				string? id = userType == "admin" ? adminId : studentId;
				if (string.IsNullOrEmpty(id))
				{
					TempData["ErrorMessage"] = "Demo user ID not configured.";
					_logger.LogWarning("Demo user ID not configured for {UserType}", userType);
					return RedirectToAction("Login");
				}
				var user = await _userManager.FindByIdAsync(id);
				if (user != null)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
					_logger.LogInformation($"Demo user {user.Email} logged in.");
					return RedirectToAction("Index", "Home");
				}
				TempData["ErrorMessage"] = $"Demo user not found.";
				_logger.LogWarning($"Demo user not found for ID: {id}");
				return RedirectToAction("Login");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "DemoSccLogin failed for userType {UserType}", userType);
				TempData["ErrorMessage"] = "An unexpected error occurred during demo login. Please try again later.";
				return RedirectToAction("Login");
			}
		}
	}
}