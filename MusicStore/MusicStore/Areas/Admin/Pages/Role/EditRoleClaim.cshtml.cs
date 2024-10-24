using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using MusicStore.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicStore.Areas.Admin.Pages.Role
{
    public class EditRoleClaimModel : RolePageModel
    {
        public EditRoleClaimModel(RoleManager<IdentityRole> roleManager, MusicDbContext musicDbContext) : base(roleManager, musicDbContext)
        {
        }
        public class InputModel
        {
            [DisplayName("Kiểu (tên) claim")]
            [Required(ErrorMessage = "Phải nhập tên của {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} ký tự")]
            public string? ClaimType { get; set; }

            [DisplayName("Giá trị")]
            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} ký tự")]
            public string? ClaimValue { get; set; }


        }
        [BindProperty]
        public InputModel? Input { get; set; }
        public IdentityRole? role { get; set; }

        IdentityRoleClaim<string> claim { get; set; }
        public async Task<IActionResult> OnGet(int? claimid)
        {
            if (claimid == null) return NotFound("Không tìm thấy role");
            var claim = _context.RoleClaims.Where(c => c.Id == claimid).FirstOrDefault();
            if (claim == null) return NotFound("Không tìm thấy role");

            role = await _roleManager.FindByIdAsync(claim.RoleId);
            if (role == null) return NotFound("Không tìm thấy role");
            Input = new InputModel()
            {
                ClaimType = claim.ClaimType,
                ClaimValue = claim.ClaimValue,
            };

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? claimid)
        {
            if (claimid == null) return NotFound("Không tìm thấy role");
            var claim = _context.RoleClaims.Where(c => c.Id == claimid).FirstOrDefault();
            if (claim == null) return NotFound("Không tìm thấy role");

            role = await _roleManager.FindByIdAsync(claim.RoleId);
            if (role == null) return NotFound("Không tìm thấy role");

            if (!ModelState.IsValid)
            {
                return Page();
            }


            if ((_context.RoleClaims.Any(c => c.RoleId == role.Id && c.ClaimType == Input.ClaimType && c.ClaimValue == Input.ClaimValue && c.Id != claim.Id)))
            {
                ModelState.AddModelError(string.Empty, "Claim này đã có trong role");
                return Page();
            }

            claim.ClaimType = Input.ClaimType;
            claim.ClaimValue = Input.ClaimValue;

            await _context.SaveChangesAsync();


            StatusMessage = "Vừa cập nhật lại claim";
            return RedirectToPage("./Edit", new { roleId = role.Id });
        }
        public async Task<IActionResult> OnPostDeleteAsync(int? claimid)
        {
            if (claimid == null) return NotFound("Không tìm thấy role");
            var claim = _context.RoleClaims.Where(c => c.Id == claimid).FirstOrDefault();
            if (claim == null) return NotFound("Không tìm thấy role");

            role = await _roleManager.FindByIdAsync(claim.RoleId);
            if (role == null) return NotFound("Không tìm thấy role");
         
            await _roleManager.RemoveClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));


            StatusMessage = "Xóa claim thành công";
            return RedirectToPage("./Edit", new { roleId = role.Id });
        }
    }
}
