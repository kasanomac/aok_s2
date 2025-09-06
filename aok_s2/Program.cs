using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using aok_s2.Areas.Identity.Data;
using aok_s2.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("aok_s2IdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'aok_s2IdentityDbContextConnection' not found.");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<aok_s2IdentityDbContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("aok_s2IdentityDbContextConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("aok_s2IdentityDbContextConnection"))
    )
);

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<aok_s2IdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
