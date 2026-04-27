using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class ResetPasswordcs
    {
        // Hidden field that carries the user ID forward
        [Required]
        public string UserID { get; set; } = "";

        // New password input
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = "";

        // Confirm password must match NewPassword
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
