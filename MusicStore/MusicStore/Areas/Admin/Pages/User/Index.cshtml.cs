using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;

namespace MusicStore.Areas.Admin.Pages.User
{
    [Authorize(Policy = "AllowEditAll")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public class UserAndRole
        {
            public string? Id { get; set; } 
            public string? UserName { get; set; }
            public string? RoleNames { get; set; }
        }       

        public List<UserAndRole>? users { get; set; }

        public async Task OnGetAsync()
        {
            var qr = _userManager.Users.OrderBy(u => u.UserName);
            var qr1 = qr.Select(u => new UserAndRole()
            {
                Id = u.Id,
                UserName = u.UserName,
            });

            users = await qr1.ToListAsync();
            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.Id))
                {
                    var appUser = await _userManager.FindByIdAsync(user.Id);
                    if (appUser != null)
                    {
                        var roles = await _userManager.GetRolesAsync(appUser);
                        user.RoleNames = string.Join(",", roles);
                    }
                    else
                    {
                        user.RoleNames = "Role không hợp lệ";
                    }
                }
                else
                {
                    user.RoleNames = "không có người dùng"; 
                }
            }
        }
        public IActionResult OnPost() => RedirectToPage();
        
    }
}
