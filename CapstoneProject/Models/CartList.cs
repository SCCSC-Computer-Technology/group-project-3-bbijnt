using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class CartList
    {
        public required List<CartItemDTO> cartList { get; set; } = new List<CartItemDTO>();
        public required string UserID { get; set; }
        public required string SpecialRequests { get; set; }
    }
}
