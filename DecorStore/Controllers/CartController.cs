using DecorStore.Helper;
using DecorStore.Models;
using DecorStore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DecorStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        private readonly DecorStoreDbContext _context;
        private readonly PaypalClient _paypalClient;
        private readonly UserManager<UserCustom> _userManager;
        public CartController(DecorStoreDbContext context, PaypalClient paypalClient, UserManager<UserCustom> userManager)
        {
            _context = context;
            _paypalClient = paypalClient;
            _userManager = userManager;
        }
        List<CartItem>? GetCartItems()
        {
            string jsoncart = HttpContext.Session.GetString("cart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }
        void SaveCartSession(List<CartItem> ls)
        {
            string jsoncart = JsonConvert.SerializeObject(ls);
            HttpContext.Session.SetString("cart", jsoncart);
        }

        public async Task<IActionResult> Index()
        {
            var carts = HttpContext.Session.GetString("cart");

            var list = new List<CartItem>();
            if (!string.IsNullOrEmpty(carts))
            {
                list = JsonConvert.DeserializeObject<List<CartItem>>(carts);
            }
            ViewBag.TongTien = list.Sum(p => p.Price * p.Quantity);
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();

            var viewModel = new CartViewModel
            {
                Categories = categories,
                CartItems = list
            };
            return View(viewModel);
        }

        public IActionResult AddItem(int ProductId, int Quantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.IdProd == ProductId);
            if (product == null)
                return BadRequest("Sản phẩm không tồn tại");
            var cart = HttpContext.Session.GetString("cart");
            if (!string.IsNullOrEmpty(cart))
            {
                var list = JsonConvert.DeserializeObject<List<CartItem>>(cart);
                if (list.Exists(x => x.Id == ProductId))
                {
                    foreach (var item in list)
                    {
                        if (item.Id == ProductId)
                        {
                            item.Quantity += Quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Id = product.IdProd;
                    item.Quantity = Quantity;
                    item.Name = product.ProdName;
                    item.Price = product.Price;
                    item.Image = product.Img1;
                    list.Add(item);
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(list));
            }
            else
            {
                var item = new CartItem();
                item.Id = product.IdProd;
                item.Quantity = Quantity;
                item.Name = product.ProdName;
                item.Price = product.Price;
                item.Image = product.Img1;
                var list = new List<CartItem>();
                list.Add(item);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(list));
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll()
        {
            HttpContext.Session.Remove("cart");
            return Json(new
            {
                status = true
            });
        }
        public IActionResult Delete(int id)
        {
            var sessionCart = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("cart"));
            sessionCart.RemoveAll(x => x.Id == id);
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(sessionCart));
            return Json(new
            {
                status = true
            });
        }
        public IActionResult UpdateCart(int id, int quantity)
        {
            var carts = GetCartItems();
            var findCartItem = carts.FirstOrDefault(p => p.Id == id);
            if (findCartItem != null)
            {
                findCartItem.Quantity = quantity;
                SaveCartSession(carts);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Payment(string name)
        {
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();
            //var cart = HttpContext.Session.GetString(CartSession);
            //var list = new List<CartItem>();
            //if (!string.IsNullOrEmpty(cart))
            //{
            //    list = JsonConvert.DeserializeObject<List<CartItem>>(cart);
            //}
            var cart = GetCartItems();
            ViewBag.TongTien = cart.Sum(p => p.Price * p.Quantity);
            var viewModel = new CartViewModel
            {
                Categories = categories,
                CartItems = cart
            };


            ViewBag.PaypalClientdId = _paypalClient.ClientId;
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Payment(string diachi, string note)
        {
            
            var cart = GetCartItems();
            var user = await _userManager.GetUserAsync(User);
            var order = new Order
            {
                OrderDate = DateTime.Now,
                IdUser = user.Id,
                TotalPrice = cart.Sum(p => p.Price * p.Quantity),
                Notes = note,
                Address = diachi,
                IsPay = false,
                IsComplete = false,
                PtThanhtoan = "Tiền mặt",
                
                OrderDetails = cart.Select(item => new OrderDetail
                {
                    IdProd = item.Id,
                    Quantity = item.Quantity,
                   
                    Price = item.Price
                }).ToList()
            };
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return Redirect("/hoan-thanh");
        }
        [Authorize]
        public async Task<IActionResult> Success()
        {
            var cart = GetCartItems();
            var categories = await _context.Categories.Where(m => m.Hide == 0).ToListAsync();
            var ViewModel = new CartViewModel
            {
                Categories = categories,
                CartItems = cart,
            };
            HttpContext.Session.Remove("cart");
            return View(ViewModel);
        }
        #region Paypal payment
        
        [HttpPost("/Cart/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
        {
            // Thông tin đơn hàng gửi qua Paypal
            var cart = GetCartItems();
            var tongTien = (cart.Sum(p => p.Price * p.Quantity)+5).ToString();
            var donViTienTe = "USD";
            var maDonHangThamChieu = "DH" + DateTime.Now.Ticks.ToString();

            try
            {
                var response = await _paypalClient.CreateOrder(tongTien, donViTienTe, maDonHangThamChieu);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }
        
        [HttpPost("/Cart/capture-paypal-order")]
        public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderID);
                var cart = GetCartItems();
                var user = await _userManager.GetUserAsync(User);
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    IdUser = user.Id,
                    TotalPrice = cart.Sum(p => p.Price * p.Quantity),
                    Notes = "da thanh toan",
                    Address = user.Address,
                    IsPay = true,
                    IsComplete = false,
                    PtThanhtoan = "Paypal",
                    OrderDetails = cart.Select(item => new OrderDetail
                    {
                        IdProd = item.Id,
                        Quantity = item.Quantity,

                        Price = item.Price
                    }).ToList()
                };
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }
        #endregion
    }
}
