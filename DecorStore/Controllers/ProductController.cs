using DecorStore.Models;
using DecorStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecorStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly DecorStoreDbContext _context;
        public ProductController(DecorStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> _NavbarPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> Index(string query, int pageNumber = 1)
        {
            int pageSize = 9;
            IQueryable<Product> productsQuery = _context.Products.Include(p => p.IdCateNavigation);
            if (!string.IsNullOrEmpty(query))
            {
                productsQuery = productsQuery.Where(p => p.ProdName.Contains(query) || p.Price.ToString().Contains(query) || p.IdProducerNavigation.ProducerName.Contains(query));
            }
            ViewBag.Query = query;
            var paginatedProducts = await PaginatedList<Product>.CreateAsync(productsQuery, pageNumber, pageSize);
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();
            //var prods = await _context.Products.Where(m => m.Hide == 0).ToListAsync();
            var viewModel = new ProductViewModel
            {
                Categories = categories,
                //Prods = prods,
                PageProducts = paginatedProducts
            };
            
            return View(viewModel);
        }
        
        public async Task<IActionResult> ProdDetail(string slug, long idProd, long idCate)
        {
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();
            var prods = await _context.Products.Where(m => m.Link == slug && m.IdProd == idProd).ToListAsync();
            var cateProds = await _context.Categories.Where(cp => cp.IdCate == idCate).FirstOrDefaultAsync();
            var list_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCate == cateProds.IdCate).ToListAsync();
            var producers = await _context.Producers.Where(m => m.Hide == 0).ToListAsync();
            if (prods == null)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = "Product Error",
                };
                return View("Error", errorViewModel);
            }
            var viewModel = new ProductViewModel
            {
                Categories = categories,
                Prods = prods,
                Prods_Recom = list_prods,
                Producers = producers
            };
            return View(viewModel);
        }
        public async Task<IActionResult> CateProd(string slug, long id)
        {

            var cateProds = await _context.Categories.Where(cp => cp.IdCate == id && cp.Link == slug).FirstOrDefaultAsync();
            if (cateProds == null)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = "CateProd Error",
                };
                return View("Error", errorViewModel);
            }
            var prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCate == cateProds.IdCate).ToListAsync();
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();

            var viewModel = new ProductViewModel
            {
                Categories = categories,
                Prods = prods,
                Catename = cateProds.CateName,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> SaleProd()
        {

            var prods = await _context.Products.Where(m => m.Hide == 0 && m.Sale > 0).ToListAsync();
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();

            var viewModel = new ProductViewModel
            {
                Categories = categories,
                Prods = prods,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> NewProd()
        {

            var prods = await _context.Products.Where(p => p.Hide == 0).GroupBy(p => p.IdCate) .Select(g => g.First()).ToListAsync();
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();

            var viewModel = new ProductViewModel
            {
                Categories = categories,
                Prods = prods,
            };
            return View(viewModel);
        }
    }
}
