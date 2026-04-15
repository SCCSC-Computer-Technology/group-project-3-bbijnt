using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CapstoneProject.Models
{
    public class StaffSignIn
    {
        [Key]
        [DisplayName("User ID")]
        public required string UserID { get; set; }
        public required string Password { get; set; }
    }
}
