using Microsoft.AspNetCore.Mvc;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Controllers
{
    [Route("api/[controller]")]

    public class ThongKeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        public ThongKeController(AppDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _context.Products.Where(x => x.IsActive).OrderByDescending(x => x.Id).ToList();
            return Json(new { data = list });
        }


    }
}
