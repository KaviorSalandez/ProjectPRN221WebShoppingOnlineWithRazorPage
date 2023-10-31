using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        [TempData]
        public string StatusMessage { get; set; }
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
    }
}
