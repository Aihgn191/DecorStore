using DecorStore.Models;

namespace DecorStore.ViewModel
{
    public class CartViewModel
    {
        public List<Category> Categories { get; set; }
        public required List<CartItem> CartItems { get; set; }
    }
}
