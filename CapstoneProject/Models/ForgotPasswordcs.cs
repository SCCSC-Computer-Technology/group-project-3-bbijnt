using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class ForgotPasswordcs
    {
        // User must enter their UserID
        [Required]
        public string UserID { get; set; } = "";

        // User must enter their email to verify identity
        [Required]
        [EmailAddress] // Ensures valid email format
        public string Email { get; set; } = "";
    }
}
