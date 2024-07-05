using DecorStore.Areas.Identity.Pages.Account;
using DecorStore.Models;
using DecorStore.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DecorStore.Controllers
{
    public class UserController : Controller
    {
        private readonly DecorStoreDbContext _context;
        private readonly UserManager<UserCustom> _userManager;
        public UserController(DecorStoreDbContext context, UserManager<UserCustom> userManager)
        {

            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> _NavbarPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> InfoUser()
        {
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();
            var user = await _userManager.GetUserAsync(User);
           
            var viewModel = new UserViewModel
            {
                Categories = categories,
                User = user
            };
            return View(viewModel);
        }
    }
    
}
