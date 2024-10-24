using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;
using System.Diagnostics;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MusicDbContext _context;

        public HomeController(ILogger<HomeController> logger, MusicDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        private List<Album> GetTopSellingAlbums(int count)
        {
            return _context.Album
                  .OrderByDescending(a => a.OrderDetails.Count())
                  .Take(count)
                  .ToList();
        }

      
        public IActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);

            return View(albums);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
