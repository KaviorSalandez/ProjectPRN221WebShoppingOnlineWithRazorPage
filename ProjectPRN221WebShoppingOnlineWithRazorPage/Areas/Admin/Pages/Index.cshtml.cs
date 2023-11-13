using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages
{
    [BindProperties]
    [Authorize(Roles = "Admin,Employee")]

    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public class ProductInTop3Sale
        {
            public string Name { get; set; }
            public int Selled { get; set; }
            public double Price { get; set; }   
        }

        public int CountProductActive { get; set; } = 0;
        public int CountUserActive { get; set; } = 0;
        public int CountCategoryActive { get; set; } = 0;
        public int CountOrderCompleted { get; set; } = 0;

        public double TotalInvestment { get; set; }
        public double TotalRevenue { get; set; }
        public double TotalProfit { get; set; }
        


        public List<Order> recentOrders { get; set; } = new List<Order>();

        public List<ProductInTop3Sale> productInTop3Sales { get; set; } = new List<ProductInTop3Sale>();

        public async Task OnGet()
        {
            CountProductActive =  await  _context.Products.Where(x => x.IsActive).CountAsync();
            CountUserActive = await _context.Users.CountAsync();
            CountCategoryActive = await _context.Categories.Where(x => (bool)x.IsActive).CountAsync();
            CountOrderCompleted = await _context.Orders.Where(x=>x.Status.Equals(Helper.SD.StatusCompleted)).CountAsync();
            recentOrders = await _context.Orders.OrderByDescending(x => x.Id).Take(5).ToListAsync();

            productInTop3Sales = _context.OrderDetails.GroupBy(od => od.Name).Select(x => new ProductInTop3Sale
            {
                Name = x.Key,
                Selled = x.Sum(od=>od.Quantity),
                Price =x.First().Price // lấy giá trị từ một bản ghi trong nhóm

            }).OrderByDescending(p => p.Selled).Take(3).ToList();


            List<Product> products = await _context.Products.ToListAsync();
            TotalInvestment = (double)products.Sum(x => (x.OriginalPrice * x.Quantity));

            TotalRevenue = await _context.Orders.Where(x => x.Status.Equals(Helper.SD.StatusCompleted)).SumAsync(x => x.OrderTotal);

            TotalProfit = TotalRevenue - TotalInvestment;


        }
    }
}
