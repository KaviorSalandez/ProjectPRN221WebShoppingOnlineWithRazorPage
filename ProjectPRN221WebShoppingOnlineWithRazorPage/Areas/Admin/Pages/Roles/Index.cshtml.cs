using ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Roles
{
    //[Authorize(Roles = "Admin,Editor")]
    public class IndexModel : RolePageModel
    {
        public IndexModel(RoleManager<IdentityRole> roleManager, AppDbContext context) : base(roleManager, context)
        {
        }
        // tạo ra một class mới kế thừa IdentityRole để thêm một vài attribute cần dùng
        public class RoleModel : IdentityRole
        {
            public string[] Claims { get; set; }
        }
        public List<RoleModel> roles { get; set; }
        // bây h mỗi role cần lấy ra các claim của nos
        public async Task OnGet()
        {
            // _roleManager.GetClaimsAsync();
            var r = await _roleManager.Roles.OrderBy(x => x.Name).ToListAsync();
            roles = new List<RoleModel>();
            foreach (var item in r)
            {
                // lấy tất cả các roleClaim của role
                var claims = await _roleManager.GetClaimsAsync(item);
                var claimsString = claims.Select(c => c.Type + "=" + c.Value); // tên và giá trị của claim
                var roleModel = new RoleModel()
                {
                    Name = item.Name,
                    Id = item.Id,
                    Claims = claimsString.ToArray(),
                };
                roles.Add(roleModel);
            }
        }
        public void OnPost() => RedirectToPage();

    }
}
