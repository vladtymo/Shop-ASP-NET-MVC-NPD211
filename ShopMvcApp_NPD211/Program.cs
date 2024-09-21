using Data;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_NPD211.MapperProfiles;
using ShopMvcApp_NPD211.Services;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Data.Entities;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("LocalDb")!;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopMvcDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => 
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ShopMvcDbContext>();

builder.Services.AddAutoMapper(typeof(AppProfile));

builder.Services.AddScoped<IFilesService, FilesService>();

builder.Services.AddDistributedMemoryCache();

// -------- sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
