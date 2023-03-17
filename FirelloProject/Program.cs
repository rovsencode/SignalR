using FirelloProject;
using FirelloProject.DAL;
using FirelloProject.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;
builder.Services.AddSignalR();

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllersWithViews();
builder.Services.FirelloServiceRegistration();
builder.Services.AddSession(options =>
{
    options.IdleTimeout= TimeSpan.FromSeconds(15);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
 );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chat");
app.Run();
