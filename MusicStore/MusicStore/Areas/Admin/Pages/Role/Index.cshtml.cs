using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;

namespace MusicStore.Areas.Admin.Pages.Role
{
    [Authorize (Policy= "AllowEditAll")]
    public class IndexModel : RolePageModel
    {
        public IndexModel(RoleManager<IdentityRole> roleManager, MusicDbContext musicDbContext)
            : base(roleManager, musicDbContext)
        {
        }
        public class RoleModel : IdentityRole
        {
            public string[]? Claims { get; set; }
        }
        public List<RoleModel>? roles { get; set; }
        public async Task OnGet()
        {
            var r = await _roleManager.Roles.OrderByDescending(r => r.Name).ToListAsync();
            roles = new List<RoleModel>();
            foreach (var _r in r)
            {
                var claims = await _roleManager.GetClaimsAsync(_r);
                var claimsString = claims.Select(c => c.Type + "=" + c.Value);

                var rm = new RoleModel()
                {
                    Name = _r.Name,
                    Id = _r.Id,
                    Claims = claimsString.ToArray()
                };
                roles.Add(rm);
            }
        }

        public void OnPost() => RedirectToPage();
    }
}
