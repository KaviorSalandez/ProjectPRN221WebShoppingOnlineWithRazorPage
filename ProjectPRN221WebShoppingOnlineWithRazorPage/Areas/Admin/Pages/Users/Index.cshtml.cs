using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public string SearchString { get; set; }

        #region Paging
        public const int ITEMS_PER_PAGE = 10;
        [BindProperty(SupportsGet = true, Name = "p")]
        // current page là trang hiện tai được tự động binding đến từ tham số tên là p
        public int currentPage { get; set; }
        // countPages là tổng số trang
        public int countPages { get; set; }
        public int totalUsers { get; set; }
        #endregion

        #region Message
        [TempData]
        public string StatusMessage { get; set; }
        #endregion

        // khai báo ra một lớp để có thể thêm thuộc tính RoleNames vào để hiển thị sagn bên Index
        public class UserAndRole : AppUser
        {
            public string RoleNames { get; set; }
        }

        public List<UserAndRole> users { get; set; }




        public async Task OnGet()
        {
            //users = await _userManager.Users.OrderBy(x => x.UserName).ToListAsync();
            var qr = _userManager.Users.OrderBy(x => x.UserName);


            totalUsers = await qr.CountAsync();
            countPages = (int)Math.Ceiling((double)totalUsers / ITEMS_PER_PAGE);

            if (currentPage < 1) currentPage = 1;
            if (currentPage > countPages) currentPage = countPages;

            var qr2 = qr.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).Select(u => new UserAndRole()
            {
                Id = u.Id,
                UserName = u.UserName,
            });

            users = await qr2.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                // nối những tên này thành một chuỗi rồi gán cho RoleNames
                user.RoleNames = string.Join(",", roles);
            }

        }



    }
}
