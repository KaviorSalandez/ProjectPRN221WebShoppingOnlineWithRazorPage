// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }


        public string Username { get; set; }

       
        [TempData]
        public string StatusMessage { get; set; }

       
        [BindProperty]
        public InputModel Input { get; set; }

       
        public class InputModel
        {
            [Phone(ErrorMessage ="{0} sai định dạng")]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Địa chỉ")]
            [StringLength(500)]
            public string HomeAddress { get; set; }
            [Display(Name = "Ngày sinh")]
            public DateTime? BirthDate { get; set; }

            public string? Avatar { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                HomeAddress = user.HomeAddress,
                BirthDate = user.BirthDate,
                Avatar = user.Avatar
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            
            user.HomeAddress = Input.HomeAddress;
            user.BirthDate = Input.BirthDate;
            user.PhoneNumber = Input.PhoneNumber;

            // cập nhật ảnh cho user
            string webRootPath = _environment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {

                var fileName = System.IO.Path.GetFileNameWithoutExtension(file[0].FileName);
                string fileName_new = Guid.NewGuid().ToString() + "_" + fileName;
                var uploads = Path.Combine(webRootPath, @"images\users");
                var extension = Path.GetExtension(file[0].FileName);

                // trước khi cập nhật cần xóa ảnh cũ đi ->th này ko cần vì ảnh default cần dùng cho những register khác
               
                // new upload
                //sao chép file vào folder
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    //sao chép vào vị trí mà chúng ta đã xác định trong our file stream
                    file[0].CopyTo(fileStream);
                }
                user.Avatar = @"\images\users\" + fileName_new + extension;
            }
            

            await _userManager.UpdateAsync(user);
            // nạp lại thông itn mới
            await _signInManager.RefreshSignInAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
