using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.ProductImages
{

    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            ListProduct = new List<ProductImage>();
        }
        [BindProperty]
        public List<ProductImage> ListProduct { get; set; }

        [BindProperty]
        public int ProductId { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id != null)
            {
                ProductId = (int)id;
            }
            ListProduct = _context.ProductImages.Where(x => x.ProductId == id).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAddImage(int id, string url)
        {
            try
            {
                _context.ProductImages.Add(new ProductImage
                {
                    ProductId = id,
                    Image = url,
                    IsDefault = false,
                    CreatedDate = DateTime.Now,
                });
                await _context.SaveChangesAsync();
                return new JsonResult(new { Success = true, Message = "Image added successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = false, Message = ex.Message });
            }
        }
        public async Task<IActionResult> OnPostDeleteImage(int id)
        {
            try
            {
                var item = await _context.ProductImages.FindAsync(id);
                if (item != null && item.IsDefault == false)
                {
                    _context.ProductImages.Remove(item);
                    await _context.SaveChangesAsync();
                    return new JsonResult(new { Success = true, Message = "Delete image successfully" });
                }
                else
                {
                    return new JsonResult(new { Success = false, Message = "Dont delete default image" });

                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = false, Message = ex.Message });
            }
        }

        //public async Task<IActionResult> OnPostFuck()   
        //{
        //    return new JsonResult(new { Success = true, Message = "Image added successfully" });
        //}
        public IActionResult OnPostChangeImageDefault(int id, int pid)
        {
            var listImage = _context.ProductImages.Where(x => x.ProductId == pid).ToList();
            foreach (var img in listImage)
            {
                img.IsDefault = false;
            }
            _context.ProductImages.UpdateRange(listImage);
            _context.SaveChanges();
            var item = _context.ProductImages.Find(id);
            if (item != null)
            {
                item.IsDefault = true;
                item.ModifiedDate = DateTime.Now;
            }
            _context.ProductImages.Update(item);


            //cập nhật lại ảnh ở product
            var productFromDb = _context.Products.FirstOrDefault(x => x.Id == pid);
            if (productFromDb != null)
            {
                productFromDb.Image = item.Image;
            }
            _context.Products.Update(productFromDb);

            _context.SaveChanges();
            return new JsonResult(new { Success = true, Message = "Change image default successfully" });
        }


    }
}
