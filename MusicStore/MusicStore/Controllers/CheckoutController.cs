using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;
using MusicStore.Service;

namespace MusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly MusicDbContext _context;
        private readonly CartService _cartService;
        private const string PromoCode = "FREE";    
        public CheckoutController(MusicDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }
        public async Task<int> CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = _cartService.GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.Album.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Album.Price);
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }

            order.Total = orderTotal;
            _context.Orders.Add(order);
            _context.SaveChanges();
            _cartService.ClearCart(); 

            return order.OrderId;
        }


        public IActionResult AddressAndPayment(Order order, string promoCode)
        {
            try
            {
                if (string.Equals(promoCode, PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    ViewData["ErrorMessage"] = "Mã khuyến mãi không hợp lệ. Vui lòng kiểm tra lại.";
                    return View(order);
                }

                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                var cart = _cartService.GetCartItems();
                _context.Orders.Add(order);
                _context.SaveChanges();
                CreateOrder(order);
                _cartService.ClearCart();

                return RedirectToAction("Complete", new { id = order.OrderId });
            }
            catch
            {
                return View(order);
            }
        }
        public IActionResult Complete(int id)
        {
            bool isValid = _context.Orders.Any( o => o.OrderId == id && o.Username == User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
            
        }
    }
}
