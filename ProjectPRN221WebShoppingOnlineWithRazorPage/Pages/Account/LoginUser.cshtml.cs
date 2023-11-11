using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages.Account
{
    public class LoginUserModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginUserModel> _logger;
        private readonly UserManager<AppUser> _userManager;

        public LoginUserModel(SignInManager<AppUser> signInManager, ILogger<LoginUserModel> logger, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {

            [DisplayName("Địa chỉ email hoặc tên tài khoản")]
            [Required(ErrorMessage = "Phải nhập {0}")]
            public string UserNameOrEmail { get; set; }


            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                //PasswordSignInAsync(string username, string password)
                // nếu ban đầu người dùng nhập vào là email thì nó sẽ trả về fasle
                var result = await _signInManager.PasswordSignInAsync(Input.UserNameOrEmail, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (!result.Succeeded)
                {
                    // cách xử lí là tìm user có địa chỉ email này r đăng nhập lại
                    var user = await _userManager.FindByEmailAsync(Input.UserNameOrEmail);
                    if (user != null)
                    {
                        // thì ta sẽ lại lấy ra đc UserName thông qua email để gắn vào PasswordSignInAsync
                        result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                    }
                }
                if (result.Succeeded)
                {
                    _logger.LogInformation("Đăng nhập thành công");
                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Tài khoản tạm thời vị khóa");
                    return RedirectToPage("./LockoutUser");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Đăng nhập thất bại");
                    ReturnUrl = "/";
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToPage("./LoginUser");
        }
    }
}
