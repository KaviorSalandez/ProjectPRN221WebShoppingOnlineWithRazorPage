using Microsoft.AspNetCore.Mvc;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Controllers
{
    // mặc dù đây là MVC Controller nhưng muốn sử dụng nó như API Controller
    // vì vậy nên thêm vào 2 thuộc tính sau

    // Đầu tiên là route thứ sẽ định nghĩa route nào bạn sẽ sử dụng để truy cập controller này
    [Route("api/[controller]")]
    [ApiController]

    // sử dụng controller điều chúng ta muốn làm là truy xuất tất cả các mục menu và quay lại làm việc với
    // bất kì thứ thứ gì lq đến db
    public class CategoryController:Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment; 
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = _context.Categories.ToList();
            return Json(new { data = list });
        }
        //xóa cate thì xóa luôn ảnh của nó trong wwwroot
        [HttpDelete("{id}")]
        public async Task<IActionResult>  Delete(int id)
        {
            var objFromDb = _context.Categories.FirstOrDefault(c => c.Id == id);

            var oldImagePath = Path.Combine(_environment.WebRootPath, objFromDb.Icon.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _context.Categories.Remove(objFromDb);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Delete success." });
        }



    }
}
