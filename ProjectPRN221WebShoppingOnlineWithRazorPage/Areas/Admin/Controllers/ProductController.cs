using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Controllers
{
        // Đầu tiên là route thứ sẽ định nghĩa route nào bạn sẽ sử dụng để truy cập controller này
        [Route("api/[controller]")]
        [ApiController]
        // sử dụng controller điều chúng ta muốn làm là truy xuất tất cả các mục product và quay lại làm việc với
        // bất kì thứ thứ gì lq đến db
        public class ProductController : Controller
        {
            private readonly IWebHostEnvironment _environment;
            private readonly AppDbContext _context;
            public ProductController(AppDbContext context, IWebHostEnvironment environment)
            {
                _environment = environment;
                _context = context;
            }

            [HttpGet]
            public IActionResult Get()
            {
                var list = _context.Products.ToList();
                return Json(new { data = list });
            }
            



        }
    }
