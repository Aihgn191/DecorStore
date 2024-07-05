using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DecorStore.Models;

namespace Search_Pagination.Controllers.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        public CartSummaryViewComponent() { }
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = GetCartItems();
            CartSummaryViewModel viewModel = new CartSummaryViewModel();
            viewModel.NumberOfItems = cart.Sum(p => p.Quantity);
            return View(viewModel);
        }
        List<CartItem> GetCartItems()
        {
            var sessionCart = HttpContext.Session.GetString("cart");
            if (sessionCart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);
            }
            return new List<CartItem>();
        }
    }
}
