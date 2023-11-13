using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Categories
{
    [Authorize(Roles = "Admin,Employee")]

    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        [TempData]
        public string StatusMessage { get; set; }
        public List<Category> ListCategory { get; set; }
        public void OnGet()
        {
            ListCategory = _context.Categories.ToList();
        }
    }
}
