using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages.Account
{
    [AllowAnonymous]

    public class ForgotPasswordConfirmationUserModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
