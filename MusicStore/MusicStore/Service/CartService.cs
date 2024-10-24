using Newtonsoft.Json;
using System.Globalization;
using MusicStore.Models;
using Microsoft.AspNetCore.Http;
using MusicStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Security.Claims;

namespace MusicStore.Service
{
    public class CartService
    {
        public const string CARTKEY = "sscart";
        private readonly ILogger<CartService> _logger;
        private readonly MusicDbContext _Dbcontext;
        private readonly IHttpContextAccessor _context;
        private readonly HttpContext? HttpContext;

        public CartService(IHttpContextAccessor context, MusicDbContext dbcontext, ILogger<CartService> logger)
        {
            _Dbcontext = dbcontext;
            _context = context;
            HttpContext = context.HttpContext;
            _logger = logger;
        }
        public List<Cart> GetCartItems()
        {
            var session = HttpContext?.Session;
            string? jsoncart = session?.GetString(CARTKEY);

            return jsoncart != null ? JsonConvert.DeserializeObject<List<Cart>>(jsoncart) ?? new List<Cart>() : new List<Cart>();
        }
        public void ClearCart()
        {
            var session = HttpContext?.Session;
            session?.Remove(CARTKEY);
        }
        public void SaveCartSession(List<Cart> cartItems)
        {
            var session = HttpContext?.Session;
            string jsoncart = JsonConvert.SerializeObject(cartItems);
            session?.SetString(CARTKEY, jsoncart);
        }
        public async Task UpdateCartAfterLoginAsync()
        {
            var cartItems = GetCartItems();
            var user = _context.HttpContext?.User;
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId) && cartItems.Any())
            {
                using var transaction = await _Dbcontext.Database.BeginTransactionAsync();

                try
                {
                    foreach (var item in cartItems)
                    {
                        var existingCart = await _Dbcontext.Carts
                            .FirstOrDefaultAsync(c => c.Id == userId && c.AlbumId == item.AlbumId);

                        if (existingCart != null)
                        {

                            existingCart.Count += item.Count;
                            _Dbcontext.Entry(existingCart).State = EntityState.Modified;
                        }
                        else
                        {
                            var newCart = new Cart
                            {
                                Id = userId,
                                AlbumId = item.AlbumId,
                                Count = item.Count,
                                DateCreated = DateTime.Now
                            };
                            _Dbcontext.Carts.Add(newCart);
                        }
                    }

                    await _Dbcontext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    _context.HttpContext.Session.Remove("CARTKEY");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error updating cart: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
