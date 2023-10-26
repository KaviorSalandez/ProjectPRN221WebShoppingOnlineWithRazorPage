using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Role
{
    public class RolePageModel:PageModel
    {
        protected readonly RoleManager<IdentityRole> _roleManager;

        protected readonly AppDbContext _context;

        [TempData]
        public string StatusMessage { get; set; }   
        public RolePageModel(RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
    }
}
