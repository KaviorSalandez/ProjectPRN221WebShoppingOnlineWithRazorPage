using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using System.Linq;

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
        public IActionResult Get(string fromDate, string toDate)
        {

            var query = from o in _context.Orders
                        join od in _context.OrderDetails on o.Id equals od.OrderId
                        join p in _context.Products on od.ProductId equals p.Id

                        select new
                        {
                            CreatedDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = p.OriginalPrice,

                        };

            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate >= startDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                endDate = endDate.AddDays(1);
                query = query.Where(x => x.CreatedDate <= endDate);
            }


            // truncatetime là bỏ thời gian đi chỉ lấy ngày tháng năm
            var result = query.GroupBy(x => x.CreatedDate.Value.Date).Select(x => new
            {
                Date = x.Key,
                Total_NhapVao = x.Sum(y => y.Quantity * y.OriginalPrice),
                Total_BanRa = x.Sum(y => y.Quantity * y.Price)
            }).Select(x => new
            {
                Date = x.Date.ToString("dd/MM/yyyy"),
                DoanhThu = x.Total_BanRa,
                LoiNhuan = x.Total_BanRa - x.Total_NhapVao
            });


            return Json(new { Success = true, dataThongKeTheoNgay = result });

        }


    }
}
