using CapstoneProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CapstoneProject.Models
{
    public class UserDetailsGetDTO
    {
        public required CapstoneProjectUser User { get; set; }
        public bool IsStaff { get; set; }
        public bool IsAdmin { get; set; }
    }
}
