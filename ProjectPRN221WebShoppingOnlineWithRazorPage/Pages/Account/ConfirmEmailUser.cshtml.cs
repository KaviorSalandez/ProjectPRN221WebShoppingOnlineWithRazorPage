using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using System.Text;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages.Account
{
   
    public class ConfirmEmailUserModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ConfirmEmailUserModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessageUser { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Không tìm thấy user có ID= '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);


            //StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";

            if (result.Succeeded)
            {
                //tiến hành đăng nhập ngay rồi bay sang trang home
                await _signInManager.SignInAsync(user, false);
                return Redirect("~/Index");
            }
            else
            {
                return Content("Lỗi xác thực email");
            }
        }
    }
}
