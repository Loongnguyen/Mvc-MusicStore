using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicStore.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MusicStore.Areas.Admin.Pages.Role
{
    public class DeleteModel : RolePageModel
    {
        public DeleteModel(RoleManager<IdentityRole> roleManager, MusicDbContext musicDbContext) : base(roleManager, musicDbContext)
        {
        }
        
        public IdentityRole? role { get; set; }
        public async Task<IActionResult> OnGet( string roleid)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            }
            return Page();
                
        }
        public async Task<IActionResult> OnPostAsync(string roleid)  
        {
            if (roleid == null) return NotFound("Không tìm thấy role");

            role = await _roleManager.FindByIdAsync(roleid);

            if (role == null) return NotFound("Không tìm thấy role");

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa Xóa role : {role.Name} thành công !";

                return RedirectToPage("./Index");
            }
            else if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }
            return Page();
        }
    }
}
