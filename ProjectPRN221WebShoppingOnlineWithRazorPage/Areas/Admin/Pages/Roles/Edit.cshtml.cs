using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Role;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class EditModel : RolePageModel
    {
        public EditModel(RoleManager<IdentityRole> roleManager, AppDbContext context) : base(roleManager, context)
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
        // khai báo 
        public List<IdentityRoleClaim<string>> Claims { get; set; }

        public IdentityRole role { get; set; }

        public async Task<IActionResult> OnGet(string roleid)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            role = await _roleManager.FindByIdAsync(roleid);
            if (role != null)
            {
                Input = new InputModel
                {
                    Name = role.Name
                };
                Claims = await _context.RoleClaims.Where(x => x.RoleId == role.Id).ToListAsync();
                return Page();
            }
            return NotFound("Không tìm thấy role");
        }
        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            role = await _roleManager.FindByIdAsync(roleid);
            if (role == null) return NotFound("Không tìm thấy role");


            // khi lấy được role đọc các claim của nó
            Claims = await _context.RoleClaims.Where(x => x.RoleId == role.Id).ToListAsync();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            role.Name = Input.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa đổi tên role: {Input.Name}";
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
