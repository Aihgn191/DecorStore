

using DecorStore.Helper;
using DecorStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DecorStoreDbContext>(options =>
 options.UseSqlServer(connectionString));
builder.Services.AddScoped<SendMail>();
//builder.Services.AddDefaultIdentity<UserCustom>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DecorStoreDbContext>();
builder.Services.AddIdentity<UserCustom, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DecorStoreDbContext>().AddDefaultTokenProviders().AddDefaultUI();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<AspNetUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<DecorStoreDbContext>();


builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton(x =>
    new PaypalClient(
        builder.Configuration["PayPalOptions:ClientId"],
        builder.Configuration["PayPalOptions:ClientSecret"],
        builder.Configuration["PayPalOptions:Mode"]
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.MapRazorPages();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "quan-tri",
      pattern: "{area:exists}/quan-tri",
      defaults: new { controller = "Products", action = "Index" } 
    );
    endpoints.MapControllerRoute(
      name: "them-san-pham",
      pattern: "{area:exists}/quan-tri/them-san-pham",
      defaults: new { controller = "Products", action = "Create" }
    );

    endpoints.MapControllerRoute(
      name: "chi-tiet",
      pattern: "{area:exists}/quan-tri/chi-tiet/{id?}",
      defaults: new { controller = "Products", action = "Details" }
    );
    endpoints.MapControllerRoute(
      name: "chinh-sua",
      pattern: "{area:exists}/quan-tri/chinh-sua/{id?}",
      defaults: new { controller = "Products", action = "Edit" }
    );
    endpoints.MapControllerRoute(
      name: "xoa-san-pham",
      pattern: "{area:exists}/quan-tri/xoa-san-pham/{id?}",
      defaults: new { controller = "Products", action = "Delete" }
    );


    endpoints.MapControllerRoute(
    name: "trang-chu",
    pattern: "trang-chu",
    defaults: new { controller = "Home", action = "Index" });

    endpoints.MapControllerRoute(
    name: "thong-tin-tai-khoan",
     pattern: "thong-tin-tai-khoan",
    defaults: new { controller = "User", action = "InfoUser" });

    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
    name: "dang-nhap",
    pattern: "dang-nhap",
    defaults: new { controller = "User", action = "Login" });

    endpoints.MapControllerRoute(
    name: "thanh-toan",
    pattern: "thanh-toan",
    defaults: new { controller = "Cart", action = "Payment" });

    endpoints.MapControllerRoute(
    name: "hoan-thanh",
    pattern: "hoan-thanh",
    defaults: new { controller = "Cart", action = "Success" });

    endpoints.MapControllerRoute(
    name: "thong-tin-cua-hang",
    pattern: "thong-tin-cua-hang",
    defaults: new { controller = "Info", action = "Index" });

    endpoints.MapControllerRoute(
    name: "lien-he",
    pattern: "lien-he",
    defaults: new { controller = "Contact", action = "Index" });

    endpoints.MapControllerRoute(
    name: "san-pham",
    pattern: "san-pham",
    defaults: new { controller = "Product", action = "Index" });

    endpoints.MapControllerRoute(
    name: "gio-hang",
    pattern: "gio-hang",
    defaults: new { controller = "Cart", action = "Index" });

    

    endpoints.MapControllerRoute(
    name: "them-gio-hang",
    pattern: "them-gio-hang",
    defaults: new { controller = "Cart", action = "AddItem" });

    endpoints.MapControllerRoute(
    name: "the-loai-san-pham",
    pattern: "{slug}-{id}",
    defaults: new { controller = "Product", action = "CateProd" });

    

    endpoints.MapControllerRoute(
    name: "chi-tiet-san-pham",
    pattern: "san-pham/{slug}-{idprod}-{idcate}",
    defaults: new { controller = "Product", action = "ProdDetail" });

    endpoints.MapControllerRoute(
    name: "sale",
    pattern: "sale",
    defaults: new { controller = "Product", action = "SaleProd" });

    endpoints.MapControllerRoute(
    name: "new",
    pattern: "new",
    defaults: new { controller = "Product", action = "NewProd" });

    

    

    

    

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    
});

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Books}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
