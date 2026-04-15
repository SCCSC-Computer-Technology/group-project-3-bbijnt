using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CapstoneProject.Areas.Identity.Data;

namespace CapstoneProject.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<CapstoneProjectUser> _signInManager;
        private readonly UserManager<CapstoneProjectUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<CapstoneProjectUser> signInManager, UserManager<CapstoneProjectUser> userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Student ID")]
            public string StudentId { get; set; }

            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        // Handles GET requests for the login page
    public async Task OnGetAsync(string? returnUrl = null)
        {
            // Show error if redirected with error message
            if (!string.IsNullOrEmpty(ErrorMessage))
                ModelState.AddModelError(string.Empty, ErrorMessage);

            // Set return URL or default to home
            returnUrl ??= Url.Content("~/");

            // Sign out any external login cookies
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Get available external login providers (Google, etc.)
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;
        }

        // Handles POST requests for login form submission
    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
                return Page();

            // Try to find user by StudentId
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.StudentId == Input.StudentId);
            if (user != null)
            {
                // Attempt password sign-in
                await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, false);
                _logger.LogInformation("User logged in with Student ID.");
                return LocalRedirect(returnUrl);
            }

            // Show error if user not found
            ModelState.AddModelError(string.Empty, "Invalid login attempt. Student ID not found.");
            return Page();
        }

        // Handles demo SSO login for admin or student demo users
        public async Task<IActionResult> OnPostDemoSccLoginAsync(string userType)
        {
            // Demo user IDs (should be in config for real apps)
            string id = userType == "admin" ? "2a51b8a1-55f7-46b9-a615-22f94c42cd07" : "03e64d67-ec61-47cd-965d-32aba793235e";
            _logger.LogWarning($"[DemoSSO] Searching for user ID: '{id}'");
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // Sign in demo user
                _logger.LogWarning($"[DemoSSO] User found: {user.Email} (ID: {user.Id})");
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation($"Demo SSO login as {user.Email}");
                return LocalRedirect(ReturnUrl ?? Url.Content("~/"));
            }
            // Show error if demo user not found
            _logger.LogWarning($"[DemoSSO] User NOT found for ID: '{id}'");
            ModelState.AddModelError(string.Empty, $"Demo user with ID {id} not found.");
            return Page();
        }
    }
}