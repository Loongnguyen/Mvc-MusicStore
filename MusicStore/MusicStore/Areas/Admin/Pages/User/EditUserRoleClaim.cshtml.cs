﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicStore.Data;
using System.Security.Claims;
using MusicStore.Models;

namespace MusicStore.Areas.Admin.Pages.User
{
    public class EditUserRoleClaimModel : PageModel
    {
        private readonly MusicDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public EditUserRoleClaimModel(MusicDbContext musicContext, UserManager<AppUser> userManager) 
        {
            _context = musicContext;
            _userManager = userManager;
        }
        [TempData]
        public string StatusMessage { get; set; }
        public NotFoundObjectResult OnGet() => NotFound("Không được truy cập");
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
        public AppUser? user { get; set; }
        public async Task<IActionResult> OnGetAddClaimAsync(string userid)
        {
           user = await _userManager.FindByIdAsync(userid);
           if(user == null) return NotFound("Không tìm thấy user");
            return Page();
        }
        public async Task<IActionResult> OnPostAddClaimAsync(string userid)
        {
            user = await _userManager.FindByIdAsync(userid);
            if (user == null) return NotFound("Không tìm thấy user");

            if(!ModelState.IsValid) return Page();
            var claims = _context.UserClaims.Where(c => c.UserId == userid);

            if(claims.Any(c => c.ClaimType == Input.ClaimType && c.ClaimValue == Input.ClaimValue))
            {
                ModelState.AddModelError(string.Empty, "Đặc tính này đã có");
                return Page();
            }
            await _userManager.AddClaimAsync(user, new Claim(Input.ClaimType, Input.ClaimValue));
            StatusMessage = "Đã thêm thuộc tính cho user";
            return RedirectToPage("./AddRole", new {Id = user.Id});
        }

        public IdentityUserClaim<string> userclaim { get; set; }
        public async Task<IActionResult> OnGetEditClaimAsync(int? claimid)
        {
            if (claimid == null) return NotFound("Không tìm thấy user");

            userclaim = _context.UserClaims.Where(c => c.Id == claimid).FirstOrDefault();

            user = await _userManager.FindByIdAsync(userclaim.UserId);

            if (user == null) return NotFound("Không tìm thấy user");

            Input = new InputModel()
            {
                ClaimType = userclaim.ClaimType,
                ClaimValue = userclaim.ClaimValue
            };
            return Page();
        }

        public async Task<IActionResult> OnPostEditClaimAsync(int? claimid)
        {
            if (claimid == null) return NotFound("Không tìm thấy user");

            userclaim = _context.UserClaims.Where(c => c.Id == claimid).FirstOrDefault();

            user = await _userManager.FindByIdAsync(userclaim.UserId);

            if (user == null) return NotFound("Không tìm thấy user");
            if(!ModelState.IsValid) return Page();

            if(_context.UserClaims.Any(c => c.UserId == user.Id 
            && c.ClaimType == Input.ClaimType 
            && c.ClaimValue == Input.ClaimValue 
            && c.Id != userclaim.Id))
            {
                ModelState.AddModelError(string.Empty, "Claim này đã có");
                    return Page();
            }

            userclaim.ClaimType = Input.ClaimType;
            userclaim.ClaimValue = Input.ClaimValue;

            await _context.SaveChangesAsync();
            StatusMessage = "Bạn vừa cập nhật claim";

            return RedirectToPage("./AddRole", new { Id = user.Id });
        }
        public async Task<IActionResult> OnPostDeleteAsync(int? claimid)
        {
            if (claimid == null) return NotFound("Không tìm thấy user");

            userclaim = _context.UserClaims.Where(c => c.Id == claimid).FirstOrDefault();

            user = await _userManager.FindByIdAsync(userclaim.UserId);

            if (user == null) return NotFound("Không tìm thấy user");

            await _userManager.RemoveClaimAsync(user, new Claim(userclaim.ClaimType, userclaim.ClaimValue));


            StatusMessage = "Xóa claim thành công";

            return RedirectToPage("./AddRole", new { Id = user.Id });
        }
    }
}
