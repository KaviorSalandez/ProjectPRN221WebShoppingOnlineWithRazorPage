using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Role;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : RolePageModel
    {
        public CreateModel(RoleManager<IdentityRole> roleManager, AppDbContext context) : base(roleManager, context)
        {
        }
        // khai báo 1 lớp con
        public class InputModel
        {
            [DisplayName("Tên của role")]
            [Required(ErrorMessage = "Phải nhập tên của role")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài từ {2} đến {1} kí tự")]
            public string Name { get; set; }

        }
        [BindProperty]
        public InputModel Input { get; set; }


        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var newRole = new IdentityRole(Input.Name);
            var result = await _roleManager.CreateAsync(newRole); // nếu có Role khác cùng tên trong Db r thì k tạo đc
            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa tạo role mới: {Input.Name}";
                return RedirectToPage("./Index");
            }
            else
            {
                result.Errors.ToList().ForEach(err =>
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                });
            }
            return Page();
        }

    }
}
