using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CapstoneProject.Areas.Identity.Data;

namespace CapstoneProject.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class ManageUsersModel : PageModel
    {
        private readonly UserManager<CapstoneProjectUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageUsersModel(
            UserManager<CapstoneProjectUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

    [BindProperty]
    public InputModel? Input { get; set; } // Nullable for safety

    [BindProperty(SupportsGet = true)]
    public string? SearchQuery { get; set; } // Nullable for safety

    [BindProperty(SupportsGet = true)]
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; } = 10;
    public int TotalUsers { get; set; }
    public List<UserViewModel>? Users { get; set; } // Nullable for safety

        // Model for assigning roles
        public class InputModel
        {
            [Required]
            [Display(Name = "Student ID")]
            public string? StudentId { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string? Role { get; set; }
        }

        // View model for displaying users
        public class UserViewModel
        {
            public string? Id { get; set; }
            public string? StudentId { get; set; }
            public string? Email { get; set; }
            public List<string>? Roles { get; set; }
        }

        // Loads the user management page with search, pagination, and roles
        public async Task OnGetAsync()
        {
            try
            {
                // Ensure Volunteer role exists
                if (!await _roleManager.RoleExistsAsync("Volunteer"))
                    await _roleManager.CreateAsync(new IdentityRole("Volunteer"));

                // Build user query with optional search
                var userQuery = _userManager.Users.AsQueryable();
                if (!string.IsNullOrWhiteSpace(SearchQuery))
                {
                    var searchLower = SearchQuery.ToLower();
                    userQuery = userQuery.Where(u => u.StudentId.ToLower().Contains(searchLower) || u.Email.ToLower().Contains(searchLower));
                }

                // Get total count for pagination
                TotalUsers = await userQuery.CountAsync();

                // Fetch paginated users
                var users = await userQuery
                    .OrderBy(u => u.StudentId)
                    .Skip((PageIndex - 1) * PageSize)
                    .Take(PageSize)
                    .Select(u => new UserViewModel
                    {
                        Id = u.Id,
                        StudentId = u.StudentId,
                        Email = u.Email,
                        Roles = new List<string>() // Will be filled below
                    })
                    .ToListAsync();

                // Fetch roles for each user efficiently
                foreach (var user in users)
                {
                    if (!string.IsNullOrEmpty(user.Id))
                    {
                        var appUser = await _userManager.FindByIdAsync(user.Id);
                        if (appUser != null)
                            user.Roles = (await _userManager.GetRolesAsync(appUser)).ToList();
                        else
                            user.Roles = new List<string>();
                    }
                    else
                    {
                        user.Roles = new List<string>();
                    }
                }

                Users = users;
            }
            catch (Exception ex)
            {
                // Log error for diagnostics
                Console.WriteLine($"Error loading users: {ex.Message}");
                throw;
            }
        }

        // Assigns a new role to a user by StudentId
        public async Task<IActionResult> OnPostAssignRoleAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            if (Input?.StudentId == null || Input.Role == null)
            {
                ModelState.AddModelError(string.Empty, "Student ID and Role are required.");
                await OnGetAsync();
                return Page();
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.StudentId == Input.StudentId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                await OnGetAsync();
                return Page();
            }

            // Remove all current roles and assign the new one
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            var result = await _userManager.AddToRoleAsync(user, Input.Role);
            if (result.Succeeded)
            {
                await OnGetAsync();
                return Page();
            }

            // Show errors if role assignment failed
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            await OnGetAsync();
            return Page();
        }

        // Edits a user's email address
        public async Task<IActionResult> OnPostEditAsync(string id, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                await OnGetAsync();
                return Page();
            }

            user.Email = email;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await OnGetAsync();
                return Page();
            }

            // Show errors if update failed
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            await OnGetAsync();
            return Page();
        }

        // Deletes a user by ID
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                await OnGetAsync();
                return Page();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await OnGetAsync();
                return Page();
            }

            // Show errors if delete failed
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            await OnGetAsync();
            return Page();
        }
    }
}