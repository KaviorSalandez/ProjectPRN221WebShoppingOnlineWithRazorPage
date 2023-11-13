using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Products
{
    [Authorize(Roles = "Admin,Employee")]

    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
            Product = new Product();
        }
        [TempData]
        public string? StatusMessage { get; set; }

        public Product Product { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public void OnGet()
        {
            // get list cate
            Categories = _context.Categories.Select(x => new SelectListItem
            {
                Text = x.Name.ToString(),
                Value = x.Id.ToString()
            }).ToList();
        }

        public async Task<IActionResult> OnPost(List<string> Images, int radioDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == radioDefault)
                        {
                            Product.Image = Images[i];
                            Product.ProductImages.Add(new ProductImage
                            {
                                ProductId = Product.Id,
                                Image = Images[i],
                                IsDefault = true,
                                CreatedDate = DateTime.Now, 
                            });
                        }
                        else
                        {
                            Product.ProductImages.Add(new ProductImage
                            {
                                ProductId = Product.Id,
                                Image = Images[i],
                                IsDefault = false,
                                CreatedDate = DateTime.Now,

                            });
                        }

                    }
                }
                Product.CreatedDate = DateTime.Now;
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();
                StatusMessage = "Thêm sản phẩm thành công";
                return RedirectToPage("./Index");

            }
            else
            {
                // get list cate
                Categories = _context.Categories.Select(x => new SelectListItem
                {
                    Text = x.Name.ToString(),
                    Value = x.Id.ToString()
                }).ToList();
                return Page();
            }
        }
    }
}
