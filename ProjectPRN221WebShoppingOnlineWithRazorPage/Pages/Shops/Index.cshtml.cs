using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Pages.Shops
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public List<Product> Products { get; set; } = new List<Product>();

        public List<Category> Categories { get; set; } = new List<Category>();

        public int CategoryIdSelected { get; set; }

        // Phân trang
        public const int ITEMS_PER_PAGE = 8;

        [BindProperty(SupportsGet = true, Name = "p")]

        public int currentPage { get; set; }

        public int countPages { get; set; }

        //Tìm kiếm theo tên
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string searchString,int cid = 0)
        {
            Categories = _context.Categories.ToList();

            var listP = _context.Products
             .Include(x => x.Category)
             .Include(x => x.ProductImages)
             .OrderByDescending(x => x.CreatedDate);

            if (cid != 0)
            {
                CategoryIdSelected = cid;
                listP = (IOrderedQueryable<Product>)listP.Where(x => x.CategoryId == cid);

            }

            if (!string.IsNullOrEmpty(searchString))
            {
                CurrentFilter = searchString;
                listP = (IOrderedQueryable<Product>)listP.Where(x => x.Title.ToLower().Contains(searchString.ToLower()));
            }

            

            int totalProduct =  listP.Count();
            countPages = (int)Math.Ceiling((double)totalProduct / ITEMS_PER_PAGE);
            if (currentPage < 1) currentPage = 1;
            if (currentPage > countPages) currentPage = countPages;


            Products =  listP.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();




            //if (cid == 0)
            //{
            //    int totalProduct = await _context.Products.CountAsync();    
            //    countPages  =  (int)Math.Ceiling((double)totalProduct / ITEMS_PER_PAGE);
            //    if (currentPage < 1) currentPage = 1;
            //    if (currentPage > countPages) currentPage = countPages;
            //    var listP = _context.Products.
            //                 Include(x => x.Category).
            //                 Include(x => x.ProductImages).
            //                 OrderByDescending(x => x.CreatedDate).
            //                 Skip((currentPage - 1) * ITEMS_PER_PAGE).
            //                 Take(ITEMS_PER_PAGE);

            //    Products = listP.ToList();
            //}
            //else
            //{
            //    CategoryIdSelected = cid;
            //    int totalProduct = await _context.Products.Where(x => x.CategoryId == cid).CountAsync();
            //    countPages = (int)Math.Ceiling((double)totalProduct / ITEMS_PER_PAGE);
            //    if (currentPage < 1) currentPage = 1;
            //    if (currentPage > countPages) currentPage = countPages;
            //    var listP = _context.Products.
            //                Include(x => x.Category).
            //                Include(x => x.ProductImages).
            //                Where(x => x.CategoryId == cid).
            //                OrderByDescending(x => x.CreatedDate).
            //                Skip((currentPage - 1) * ITEMS_PER_PAGE).
            //                Take(ITEMS_PER_PAGE);
            //    Products = listP.ToList();
            //}

            //if(searchString != null)
            //{
            //    CurrentFilter = searchString;
            //    Products = Products.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
            //}
        }
    }
}
