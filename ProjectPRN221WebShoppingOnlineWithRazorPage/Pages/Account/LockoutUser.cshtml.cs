using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.AspNetCore.Authorization;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages.Account
{
    [AllowAnonymous]
    public class LockoutUserModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
