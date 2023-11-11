using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages.Products
{
    [BindProperties]
    public class DetailModel : PageModel
    {

        public readonly AppDbContext _context;
        public DetailModel(AppDbContext context)
        {
            _context = context;     
        }
        public Product Product { get; set; }
        public List<ProductImage>  productImages { get; set; }
        public void OnGet(int? id)
        {
            Product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (Product != null)
            {
                productImages = _context.ProductImages.Where(x=>x.ProductId == Product.Id).ToList();
            }
        }
    }
}
