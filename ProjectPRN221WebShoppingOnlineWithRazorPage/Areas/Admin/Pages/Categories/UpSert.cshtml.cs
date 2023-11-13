using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;

namespace ProjectPRN221WebShoppingOnlineWithRazorPage.Areas.Admin.Pages.Categories
{
    [BindProperties]
    [Authorize(Roles = "Admin,Employee")]

    public class UpSertModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;

        public UpSertModel(AppDbContext context, IWebHostEnvironment environment)
        {
            _context  = context;
            _environment = environment;
            Category = new Category();
        }
        [TempData]
        public string StatusMessage { get; set; }
        public Category Category { get; set; }
        public IActionResult OnGetAsync(int? id)
        {
            if (id != null)
            {
                //Edit
                Category = _context.Categories.FirstOrDefault(x => x.Id == id);
                if (Category == null) return NotFound("Not found category");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string webRootPath = _environment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (Category.Id == 0)
            {
                //create
                var fileName = System.IO.Path.GetFileNameWithoutExtension(files[0].FileName);
                string fileName_new = Guid.NewGuid().ToString()+"_"+ fileName;
                var uploads = Path.Combine(webRootPath, @"images\category");
                var extension = Path.GetExtension(files[0].FileName);
                //sao chép file vào folder
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    //sao chép vào vị trí mà chúng ta đã xác định trong our file stream, bên trong mục Images/MenuItem
                    files[0].CopyTo(fileStream);
                }
                Category.Icon = @"\images\category\" + fileName_new + extension;
                Category.CreatedDate = DateTime.Now;
                Category.IsActive = true;
                await _context.Categories.AddAsync(Category);
                await _context.SaveChangesAsync();
                StatusMessage = "Thêm mới sản phẩm thành công";
            }
            else
            {
                //edit
                var objFromDb = _context.Categories.FirstOrDefault(x => x.Id == Category.Id);
                if (files.Count > 0)
                {

                    var fileName = System.IO.Path.GetFileNameWithoutExtension(files[0].FileName);
                    string fileName_new = Guid.NewGuid().ToString()+ "_" + fileName;
                    var uploads = Path.Combine(webRootPath, @"images\category");
                    var extension = Path.GetExtension(files[0].FileName);
                    // trước khi cập nhật cần xóa ảnh cũ đi 
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Icon.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    // new upload
                    //sao chép file vào folder
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        //sao chép vào vị trí mà chúng ta đã xác định trong our file stream, bên trong mục Images/MenuItem
                        files[0].CopyTo(fileStream);
                    }
                    Category.Icon = @"\images\category\" + fileName_new + extension;
                }
                else
                {
                    Category.Icon = objFromDb.Icon;
                }
                Category.IsActive = objFromDb.IsActive;
                Category.CreatedDate = objFromDb.CreatedDate;
                Category.ModifiedDate = DateTime.Now;
                _context.Categories.Update(Category);
                await _context.SaveChangesAsync();
                StatusMessage = "Cập nhật sản phẩm thành công";

            }
            return RedirectToPage("./Index");
        }


    }
}
