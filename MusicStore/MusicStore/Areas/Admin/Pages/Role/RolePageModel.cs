using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicStore.Data;
namespace MusicStore.Areas.Admin.Pages.Role
{
    public class RolePageModel : PageModel
    {
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly MusicDbContext _context;
        [TempData]
        public string? StatusMessage { get; set; }
        public RolePageModel(RoleManager<IdentityRole> roleManager, MusicDbContext musicDbContext) 
        {
             _roleManager = roleManager;
             _context = musicDbContext; 
        }
    }
}
