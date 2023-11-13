using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Orders
{
    [BindProperties]

    public class DetailModel : PageModel
    {
        private readonly AppDbContext _context;
        [TempData]
        public string? StatusMessage { get; set; }
        public Order Order { get; set; }
        private readonly IWebHostEnvironment _environment;
        public DetailModel(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Order = new Order();
            _environment = environment;
        }
        public IActionResult OnGet(int? id)
        {
            if(id  == null)
            {
                return NotFound();
            }
            else
            {
                Order = _context.Orders.Include(x=>x.OrderDetails).FirstOrDefault(x=>x.Id == id);
                return Page();
            }
        }
    }
}
