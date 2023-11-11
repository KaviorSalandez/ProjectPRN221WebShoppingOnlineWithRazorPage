using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationUserModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
