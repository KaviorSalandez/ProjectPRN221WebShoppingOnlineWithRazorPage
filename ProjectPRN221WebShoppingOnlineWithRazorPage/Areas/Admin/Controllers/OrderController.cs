using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Helper;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(AppDbContext context, IWebHostEnvironment environment, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _environment = environment;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public class OrderViewModel
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public double OrderTotal { get; set; }
            public string Comment { get; set; }
            public string Status { get; set; }
            public string ShipAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string SessionId { get; set; }
            public string PaymentIntentId { get; set; }
            public string CustomerName { get; set; }
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _context.Orders.Where(x => x.Status == SD.StatusCompleted).Include(x=>x.AppUser).Select(x => new OrderViewModel
            {
                Id = x.Id,
                Code = x.Code,
                OrderTotal = x.OrderTotal,
                Comment = x.Comment,
                Status = x.Status,
                ShipAddress = x.ShipAddress,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                SessionId = x.SessionId,
                PaymentIntentId = x.PaymentIntentId,
                CustomerName = x.AppUser.UserName
            }).OrderByDescending(x=>x.Id).ToListAsync();

            return Json(new { data = list });
        }
    }
}
