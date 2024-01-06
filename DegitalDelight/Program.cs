using DegitalDelight.Data;
using DegitalDelight.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using DegitalDelight.Services.Interfaces;
using DegitalDelight.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Areas.Admin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

    options.User.RequireUniqueEmail = true;
})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddHangfire((sp, config) =>
{
    config.UseSqlServerStorage(connectionString);
});
builder.Services.AddHangfireServer(); 
builder.Services.AddHttpContextAccessor();
builder.Services.TryAddScoped<UserManager<User>>();

//Cac service
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddTransient<IDiscountService, DiscountService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddScoped<IProduct, ProductsService>();
builder.Services.AddScoped<ISupply, SupplyService>();
builder.Services.AddScoped<IComment, CommentService>();
builder.Services.AddTransient<IDiscount, DiscountsService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(RedirectToDefaultActionFilter));
});

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
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/NotFound";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseHangfireDashboard();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Admin}/{action=Dashboard}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Homepage}/{id?}");

app.MapRazorPages();



RecurringJob.AddOrUpdate<DiscountService>("SundayDiscount", x => x.SundayDiscount(), "0 0 * * 0");

app.Run();
