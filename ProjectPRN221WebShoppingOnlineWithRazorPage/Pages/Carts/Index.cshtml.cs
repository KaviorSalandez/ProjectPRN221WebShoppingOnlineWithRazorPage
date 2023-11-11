using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Helper;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using ProjectPRN221WebShoppingOnlineWithRazorPage.ViewModel;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages.Carts
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        public readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public List<Item> listItemInCart { get; set; }
        public IndexModel(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public ShoppingCart Cart { get; set; }


        public void OnGet()
        {
            Cart = SessionHelper.GetObjectFromJson<ShoppingCart>(_httpContextAccessor.HttpContext.Session, "cart");
        }

		public async Task<IActionResult> OnPostAddToCart(int id, int quantity)
        {
            var code = new JsonResult(new { success = false, data = 0 });
            var itemcheck = _context.Products.Include(x=>x.Category).FirstOrDefault(x => x.Id == id);
			if (itemcheck != null)
            {
                Cart = SessionHelper.GetObjectFromJson<ShoppingCart>(_httpContextAccessor.HttpContext.Session,"cart");
                if(Cart == null)
                {
                    Cart = new ShoppingCart();
                }
                Item item = new Item
                {
                    ProductId = itemcheck.Id,
                    ProductName = itemcheck.Title,
                    CategoryName = itemcheck.Category.Name,
                    Quantity = quantity,
                    ImageOfProduct = itemcheck.Image
                };
                if (itemcheck.PriceSale > 0)
                {
                    item.Price = (decimal)itemcheck.PriceSale;
                }
                else
                {
                    item.Price = (decimal)itemcheck.Price;
                }
                item.PriceTotal = item.Quantity * item.Price;
                Cart.AddToCart(item,quantity);
                int count = Cart.Items.Count();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                code = new JsonResult(new { success = true, data = count });
            }
            return code;
        }
        public async Task<IActionResult> OnGetShowCount()
        {
            Cart = SessionHelper.GetObjectFromJson<ShoppingCart>(_httpContextAccessor.HttpContext.Session, "cart");
            if(Cart != null) {
                return new JsonResult(new { success = true, data = Cart.Items.Count });
            }
            else
            {
                return new JsonResult(new { success = true, data = 0 });

            }
        }
        public async Task<IActionResult> OnGetDeleteItem(int id)
        {
            var code =  new JsonResult(new { success = false, data = 0 });
            Cart = SessionHelper.GetObjectFromJson<ShoppingCart>(_httpContextAccessor.HttpContext.Session, "cart");
            if (Cart != null)
            {
                var checkProduct = Cart.Items.FirstOrDefault(x => x.ProductId == id);
                if(checkProduct != null)
                {
                    Cart.Items.Remove(checkProduct);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                    code = new JsonResult(new { success = true, data = Cart.Items.Count() });
                }
            }
            return code;
        }


        public async Task<IActionResult> OnGetDeleteAllItem()
        {
            var code = new JsonResult(new { success = false, data = 0 });
            Cart = SessionHelper.GetObjectFromJson<ShoppingCart>(_httpContextAccessor.HttpContext.Session, "cart");
            if (Cart != null)
            {
                    Cart.ClearCart();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                    code = new JsonResult(new { success = true});
            }
            return code;
        }

        public async Task<IActionResult> OnGetUpdateQuantityOfItem(int id, int quantity)
        {
            var code = new JsonResult(new { success = false, data = 0 });
            Cart = SessionHelper.GetObjectFromJson<ShoppingCart>(_httpContextAccessor.HttpContext.Session, "cart");
            if (Cart != null)
            {
                Cart.UpdateQuantity(id, quantity);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                code = new JsonResult(new { success = true });
            }
            return code;
        }





    }
}
