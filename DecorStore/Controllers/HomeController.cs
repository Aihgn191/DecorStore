using DecorStore.Models;
using DecorStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DecorStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly DecorStoreDbContext _context;

        public HomeController(DecorStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> _NavbarPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();
            var producers = await _context.Producers.Where(m => m.Hide == 0).ToListAsync();
            var products = await _context.Products.Where(m => m.Hide == 0).ToListAsync();
            
            products.Reverse();
            var viewModel = new HomeViewModel
            {
                Categories = categories,
                Producers = producers,
                Products = products,
            };
            return View(viewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
