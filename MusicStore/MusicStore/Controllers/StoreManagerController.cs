using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using MusicStore.Data;
using Microsoft.AspNetCore.Authorization;

namespace MusicStore.Controllers
{
    [Authorize(Policy = "AllowEdit")]
    public class StoreManagerController : Controller
    {
        private readonly MusicDbContext _context;

        //public const int ITEMS_PER_PAGE = 10;
        //[BindProperty(SupportsGet = true, Name = "p")]
        //public int currentpage { get; set; }
        //public int countpages { get; set; }
        public StoreManagerController(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            //int totalAlbum = await _context.Album.CountAsync();
            //countpages = (int)Math.Ceiling((double)totalAlbum / ITEMS_PER_PAGE);

            //if (currentpage < 1)
            //    currentpage = 1;
            //if (currentpage > countpages)
            //    currentpage = countpages;

            var albums = from a in _context.Album
                         .Include(a => a.Genre)
                         .Include(a => a.Artist)
                         select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(a => a.Title.Contains(searchString));
            }

            var albumList = await albums.ToListAsync();
            if (!albumList.Any())
            {
                ViewBag.Message = "Không tìm thấy album nào.";
            }

            ViewBag.SearchString = searchString;

            return View(albumList);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Genre)
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Genre)
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            ViewBag.GenreId = new SelectList(_context.Genre, "GenreId", "GenreName", album.GenreId);
            ViewBag.ArtistId = new SelectList(_context.Artist, "ArtistId", "ArtistName", album.ArtistId);
            return View(album);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumId))
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

            ViewBag.GenreId = new SelectList(_context.Genre, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_context.Artist, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }
        public IActionResult Create()
        {
            ViewBag.Genre = new SelectList(_context.Genre, "GenreId", "GenreName");
            ViewBag.Artist = new SelectList(_context.Artist, "ArtistId", "ArtistName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Genre = new SelectList(_context.Genre, "GenreId", "GenreName");
            ViewBag.Artist = new SelectList(_context.Artist, "ArtistId", "ArtistName");
            return View(album);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
               .Include(a => a.Genre)
               .Include(a => a.Artist)
               .FirstOrDefaultAsync(m => m.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            _context.Album.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.AlbumId == id);
        }
    }
}
