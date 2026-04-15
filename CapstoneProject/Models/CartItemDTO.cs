using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CapstoneProject.Models
{
    public class CartItemDTO
    {
        public required Item Item { get; set; }
        public required CartItemInfo Info { get; set; }
    }
}
