using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class ScheduledDonation
    {
        [Key]
        public int ScheduledDonationID { get; set; }

        [Required]
        public DateTime ScheduledDropOffTime { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = "";

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = "";

        public string? Company { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        public string? Phone { get; set; } = "";

        [Required]
        [DisplayName("Donation Description")]
        public string DonationDescription { get; set; } = "";

        public string? Notes { get; set; } = "";

        public bool IsCompleted { get; set; } = false;

        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}
