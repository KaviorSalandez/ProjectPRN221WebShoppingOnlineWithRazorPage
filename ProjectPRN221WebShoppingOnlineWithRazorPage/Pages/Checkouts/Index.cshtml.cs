using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Helper;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using ProjectPRN221WebShoppingOnlineWithRazorPage.ViewModel;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages.Checkouts
{
    [BindProperties]
    [Authorize]

    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public ShoppingCart Cart { get; set; }
        public OrderViewModel OrderViewModel { get; set; }

        public IndexModel(AppDbContext context, IHttpContextAccessor httpContextAccessor,SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
             Cart = new ShoppingCart();
        }
        public async Task OnGet()
        {
            OrderViewModel = new OrderViewModel();
            Cart = SessionHelper.GetObjectFromJson<ShoppingCart>(_httpContextAccessor.HttpContext.Session, "cart");
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                OrderViewModel.UserId = user.Id;
                OrderViewModel.Address = user.HomeAddress ?? "";
                OrderViewModel.PhoneNumber = user.PhoneNumber ?? "";
                OrderViewModel.CustomerName = user.UserName;
                OrderViewModel.Email = user.Email;
            }
        }

        public IActionResult OnPostThanhToan()
        {
            if (ModelState.IsValid)
            {
                Cart = SessionHelper.GetObjectFromJson<ShoppingCart>(_httpContextAccessor.HttpContext.Session, "cart");
                // tổng số mặt hàng có trong đơn hàng
                int quantity = Cart.Items.Count();
                if (Cart.Items != null && Cart.Items.Any())
                {
                    Order order = new Order();
                    order.UserId = OrderViewModel.UserId;
                    order.ShipAddress = OrderViewModel.Address;
                    order.PhoneNumber = OrderViewModel.PhoneNumber;
                    order.Comment = OrderViewModel.Comment;
                    order.Email = OrderViewModel.Email;
                    order.Code = "DH#" + Guid.NewGuid().ToString();
                    order.CreatedDate = DateTime.Now;

                    Cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = (double)x.Price,
                        Name = x.ProductName
                    }));

                    order.OrderTotal = (double)Cart.GetPriceTotal();
                    order.Status = Helper.SD.StatusCompleted;
                    _context.Add(order);
                    _context.SaveChanges();

                    // giảm số lượng sản phẩm sau khi đặt hàng
                    //foreach (var item in listItemInCart)
                    //{
                    //    var p = _northWindContext.Products.Find(item.ProductItem.ProductId);
                    //    if (p != null)
                    //    {
                    //        p.UnitsInStock = (short?)(p.UnitsInStock - item.Quantity);
                    //    }
                    //}
                    //_northWindContext.SaveChanges();


                    // xóa giỏ hàng hiện tại
                    Cart.ClearCart();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);

                }

                return RedirectToPage("./SuccessOrder");

            }
            else
            {
                return Page();
            }

        }






    }
}
