using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class SetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SetPasswordModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

       
        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
           
            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} kí tự", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu mới")]
            public string NewPassword { get; set; }

            
            [DataType(DataType.Password)]
            [Display(Name = "Xác nhận mật khẩu")]
            [Compare("NewPassword", ErrorMessage = "Mật khẩu lặp lại ko chính xác")]
            public string ConfirmPassword { get; set; }
        }


        public AppUser user { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Không có user");
            }
            user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Không thấy user với id = {id}");
            }
            // trả về trang set pass để set pas cho user vừa tìm thấy
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Không có user");
            }
            user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Không thấy user với id = {id}");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // chú ý khi thêm mật khẩu cho user thì user đó phải ko có mật khẩu thì code dưới mới có thể thực hiện
            // => nên loại bỏ mật khẩu của user đó đã để tránh bị lỗi
            await _userManager.RemovePasswordAsync(user);


            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            StatusMessage = $"Vừa cập nhật mật khẩu cho user: {user.UserName}";

            return RedirectToPage("./Index");
        }
    }
}
