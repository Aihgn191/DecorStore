using DecorStore.Models;
using DecorStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecorStore.Controllers
{
    public class ContactController : Controller
    {
        private readonly DecorStoreDbContext _context;
        public ContactController(DecorStoreDbContext context)
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

            var viewModel = new ContactViewModel
            {
                Categories = categories

            };
            return View(viewModel);
        }
    }
}
