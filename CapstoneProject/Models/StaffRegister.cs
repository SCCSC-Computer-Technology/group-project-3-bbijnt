using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CapstoneProject.Models
{
    public class StaffRegister
    {
        [Key]
        [DisplayName("User ID")]
        [Required]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "ID must have 7 digits")]
        public required string UserID { get; set; }
        [Required]
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
