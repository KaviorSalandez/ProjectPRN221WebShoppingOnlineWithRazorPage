using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : Controller
    {
        private readonly AppDbContext _context;
        public ProductImageController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { Success = true, Message = "Image added successfully" });
        }
        [HttpPost]
        public IActionResult Post(int id, string url)
        {
            try
            {
                _context.ProductImages.Add(new ProductImage
                {
                    ProductId = id,
                    Image = url,
                    IsDefault = false
                });
                 _context.SaveChanges();
                return Json(new { Success = true, Message = "Image added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Fuck()
        {
            return Json(new { Success = true, Message = "Image added successfully" });

        }

    }
}
