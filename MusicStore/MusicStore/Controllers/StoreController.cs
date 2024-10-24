using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using MusicStore.Models;
using Microsoft.EntityFrameworkCore;
using MusicStore.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly MusicDbContext _context;
        private readonly CartService _cartService;
        public StoreController(MusicDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            var genres = await _context.Genre.ToListAsync();
            return View(genres);
        }
        public async Task<IActionResult> Browse(string genre)
        {
            var genreModel = await _context.Genre
                .Include(g => g.Album)
                .SingleOrDefaultAsync(g => g.GenreName == genre);

            return genreModel == null ? NotFound() : View(genreModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var album = await _context.Album
                                      .Include(a => a.Genre)
                                      .Include(a => a.Artist)
                                      .FirstOrDefaultAsync(a => a.AlbumId == id);

            return album == null ? NotFound() : View(album);
        }
        public async Task<IActionResult> GenreMenu()
        {
            var genres = await _context.Genre.ToListAsync();
            return PartialView(genres);
        }
       
    }
}
