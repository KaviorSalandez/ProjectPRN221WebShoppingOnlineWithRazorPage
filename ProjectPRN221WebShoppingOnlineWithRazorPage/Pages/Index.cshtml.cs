using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {

        public readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public List<Category> Categories { get; set; }

        public List<Product> ProductInFeatures { get; set; }    
      
        public List<Product> ProductInSales { get; set; }   

        public async Task OnGet()
        {
            Categories = await _context.Categories.ToListAsync();
            ProductInFeatures  = await _context.Products.Where(x=>x.IsActive && x.IsFeature==true).Include(x=>x.Category).Include(x=>x.ProductImages).Take(16).ToListAsync();
            ProductInSales  = await _context.Products.Where(x => x.IsActive && x.IsSale == true).Include(x => x.Category).Include(x => x.ProductImages).Take(10).ToListAsync();
        }
    }
}