using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MusicStore.Areas.Admin.Pages.Role
{
    [Authorize(Policy = "AllowEditAll")]
    public class EditModel : RolePageModel  
    {
        public EditModel(RoleManager<IdentityRole> roleManager, MusicDbContext musicDbContext) : base(roleManager, musicDbContext)
        {
        }
        public class InputModel
        {
            [DisplayName("Tên Của role")]
            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} ký tự")]
            public string? Name { get; set; }
        
        
        }
        [BindProperty]
        public InputModel? Input { get; set; }
        public List<IdentityRoleClaim<string>>? Claims {  get; set; } 
        public IdentityRole? role { get; set; }
        public async Task<IActionResult> OnGet( string roleid)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            role = await _roleManager.FindByIdAsync(roleid);
            if (role != null)
            {
                Input = new InputModel()
                {
                    Name = role.Name,
                };
                Claims = await _context.RoleClaims.Where(rc => rc.RoleId == roleid).ToListAsync();
                return Page();
            } 
            return NotFound("Không tìm thấy role");
                
        }
        public async Task<IActionResult> OnPostAsync(string roleid)  
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            role = await _roleManager.FindByIdAsync(roleid);
            if (roleid == null) return NotFound("Không tìm thấy role");
            Claims = await _context.RoleClaims.Where(rc => rc.RoleId == roleid).ToListAsync();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            role.Name = Input.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa đổi tên role thành: {Input.Name} thành công !";

                return RedirectToPage("./Index");
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
