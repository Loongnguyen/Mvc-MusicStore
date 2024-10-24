using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;
using MusicStore.Service;

namespace MusicStore.Controllers
{
    public class CartController : Controller
    {
        private readonly MusicDbContext _context;
        private readonly CartService _cartService;
        public CartController(MusicDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }
        [Route("addcart/{albumid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int albumid)
        {

            var album = _context.Album
                .Where(p => p.AlbumId == albumid)
                .FirstOrDefault();
            if (album == null)
                return NotFound("Không có sản phẩm");

            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(c => c.Album.AlbumId == albumid);
            if (cartitem != null)
            {
                cartitem.Count++;
            }
            else
            {
                cart.Add(new Cart() { Count = 1, Album = album });
            }
            _cartService.SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(_cartService.GetCartItems());
        }
        [Route("/removecart/{albumid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int albumid)
        {
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(a => a.Album.AlbumId == albumid);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            _cartService.SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int albumid, [FromForm] int quantity)
        {
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(a => a.Album.AlbumId == albumid);
            if (cartitem != null)
            {
                cartitem.Count = quantity;
            }
            _cartService.SaveCartSession(cart);
            return Ok();
        }
    }
}
