using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Products
{
    [Authorize(Roles = "Admin,Employee")]

    [BindProperties]
    public class UpdateModel : PageModel
    {
        private readonly AppDbContext _context;
        [TempData]
        public string? StatusMessage { get; set; }
        public Product Product { get; set; }
        private readonly IWebHostEnvironment _environment;


        public List<SelectListItem> Categories { get; set; }
        public UpdateModel(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Product = new Product();
            _environment = environment;
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null)
            {
                //Edit
                Product = _context.Products.FirstOrDefault(x => x.Id == id);
                if (Product == null) return NotFound("Not found product");
            }
            // get list cate
            Categories = _context.Categories.Select(x => new SelectListItem
            {
                Text = x.Name.ToString(),
                Value = x.Id.ToString()
            }).ToList();

            return Page();
        }
        public async Task<IActionResult>  OnPost(int? id)
        {
            if(!ModelState.IsValid) {
                return Page();
            }
            else
            {
                Categories = _context.Categories.Select(x => new SelectListItem
                {
                    Text = x.Name.ToString(),
                    Value = x.Id.ToString()
                }).ToList();

                if (id != null)
                {
                    //Edit
                    var objectFromDb = _context.Products.FirstOrDefault(x => x.Id == id);
                    if (objectFromDb == null) return NotFound("Not found product");
                    else
                    {
                        // ẢNH THÌ VẪN GIỮ NGUYÊN Ở ĐÂY VÌ MÌNH QUẢN LÍ ẢNH Ở CHỖ KHÁC
                        Product.Image = objectFromDb.Image;
                        Product.ModifiedDate = DateTime.Now;
                        Product.CreatedDate = objectFromDb.CreatedDate;
                        _context.Products.Update(Product);
                        await _context.SaveChangesAsync();
                        StatusMessage = $"Cập nhật thành công";
                        return RedirectToPage("./Index");
                    }
                }
                return NotFound("Không tìm thấy Id sản phẩm cần update");
            }
        }

    }
}
