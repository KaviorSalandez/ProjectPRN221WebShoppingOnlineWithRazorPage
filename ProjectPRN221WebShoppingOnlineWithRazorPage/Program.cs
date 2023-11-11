using Microsoft.EntityFrameworkCore;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Models;
using System;
using Microsoft.AspNetCore.Identity;
using ProjectPRN221WebShoppingOnlineWithRazorPage.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectString = builder.Configuration.GetConnectionString("MyCnn");
    options.UseSqlServer(connectString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // thiết lập đường dẫn đến trang login và logout của ứng dụng
    options.LoginPath = "/admin/login";
    options.LogoutPath = "/admin/logout";
    //đường dẫn tới trang khi user bị cấm truy cập
    options.AccessDeniedPath = "/accessdenied.html";
});
builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lần thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     
    options.SignIn.RequireConfirmedAccount = true; // email phải confirm mới được đăng nhập

    

});
// cấu hình dịch vụ gửi email trong Service
var mailSetting = builder.Configuration.GetSection("MailSettings");
// đăng kí một lớp cấu hình MailSetting và đc cấu hình từ mailSetting đọc được
builder.Services.Configure<MailSettings>(mailSetting);
// => Trong hệ thống DI sẽ có MailSettings, MailSetttings này sẽ đưuọc inject
// vào SendMailServices khi dịch vụ này được tạo ra
// tiếp theo phải đăng kí dịch vụ SendMailServices
// Singleton được tạo một lần và nó tồn tại suốt quá trình hoạt động của ứng dụng
// như vậy trong hệ thống có một dichjvuj tên là IEmailSender. Khi dịch vụ này được lấy ra thì đó là đối tượng SendMailService
builder.Services.AddSingleton<IEmailSender, SendMailService>();
// add thêm dịch vụ Custom Error khi tạo role riêng IdentityErrorDescriber
// 2 tham số dịch vụ ban đầu là: IdentityErrorDescriber và nó lấy ra cái AppIdentityErrorDescriber
builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOptions();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.AddSession();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// /contents/a1.jpg => Uploads/a1.jpg
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(),"Uploads")
        ),
    RequestPath = "/contents"
});

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.UseSession();

app.MapRazorPages();
// để sử dụng API và lấy đến đúng route cần khai báo
app.MapControllers();

app.Run();
