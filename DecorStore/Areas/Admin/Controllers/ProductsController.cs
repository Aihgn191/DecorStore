using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DecorStore.Models;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace DecorStore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly DecorStoreDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductsController(DecorStoreDbContext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(string query, int pageNumber = 1)
        {
            
            int pageSize = 8;
            IQueryable<Product> productsQuery = _context.Products
         .Include(p => p.IdCateNavigation)
         .Include(p => p.IdProducerNavigation);

            if (!string.IsNullOrEmpty(query))
            {
                productsQuery = productsQuery.Where(p => p.ProdName.Contains(query) || p.Price.ToString().Contains(query) || p.IdProducerNavigation.ProducerName.Contains(query));
            }

            var paginatedProducts = await PaginatedList<Product>.CreateAsync(productsQuery, pageNumber, pageSize);

            // Pass the query string to the view so it's maintained during pagination
            ViewBag.Query = query;

            return View(paginatedProducts);
        }




        public List<string> SearchSuggestions(string query)
        {

            return _context.Products.Where(p => p.ProdName.StartsWith(query)).Select(p => p.ProdName).ToList();

        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.IdCateNavigation)
                .Include(p => p.IdProducerNavigation)
                .FirstOrDefaultAsync(m => m.IdProd == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["IdCate"] = new SelectList(_context.Categories, "IdCate", "CateName");
            ViewData["IdProducer"] = new SelectList(_context.Producers, "IdProducer", "ProducerName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product, IFormFile file1, IFormFile file2, IFormFile file3)
        {

            
            ModelState.Remove("IdCateNavigation");
            ModelState.Remove("IdProducerNavigation");
            if (ModelState.IsValid)
            {
                product.IdProd = _context.Products.Count() > 0 ? _context.Products.Max(item => item.IdProd) + 1 : 1;
                if (file1 != null && file1.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
                        //đổi tên sau khi upload
                        var filePath = Path.Combine(uploadsFolder, file1.FileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file1.CopyToAsync(fileStream);
                        }
                        product.Img1 = file1.FileName;
                    }
                if (file2 != null && file2.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
                    //đổi tên sau khi upload
                    var filePath = Path.Combine(uploadsFolder, file2.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file2.CopyToAsync(fileStream);
                    }
                    product.Img2 = file2.FileName;
                }
                if (file3 != null && file3.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
                    //đổi tên sau khi upload
                    var filePath = Path.Combine(uploadsFolder, file3.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file3.CopyToAsync(fileStream);
                    }
                    product.Img3 = file3.FileName;
                }
                product.Nsx = DateTime.Now;
                product.Hide = 0;
                product.Sale = 0;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCate"] = new SelectList(_context.Categories, "IdCate", "IdCate", product.IdCate);
            ViewData["IdProducer"] = new SelectList(_context.Producers, "IdProducer", "IdProducer", product.IdProducer);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["IdCate"] = new SelectList(_context.Categories, "IdCate", "IdCate", product.IdCate);
            ViewData["IdProducer"] = new SelectList(_context.Producers, "IdProducer", "IdProducer", product.IdProducer);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProd,ProdName,AliasName,IdCate,IdProducer,Unit,Price,Img1,Img2,Img3,Nsx,Sale,Description,Link,Hide,Nums")] Product product)
        {
            if (id != product.IdProd)
            {
                return NotFound();
            }
            ModelState.Remove("IdCateNavigation");
            ModelState.Remove("IdProducerNavigation");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCate"] = new SelectList(_context.Categories, "IdCate", "IdCate", product.IdCate);
            ViewData["IdProducer"] = new SelectList(_context.Producers, "IdProducer", "IdProducer", product.IdProducer);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.IdCateNavigation)
                .Include(p => p.IdProducerNavigation)
                .FirstOrDefaultAsync(m => m.IdProd == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.IdProd == id);
        }
    }
}
