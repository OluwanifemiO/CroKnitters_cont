using CroKnitters.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = false;
});

//connect to the db

var connStr = builder.Configuration.GetConnectionString("DBconnectionString");
builder.Services.AddDbContext<CrochetAppDbContext>(options => options.UseSqlServer(connStr));

builder.Services.AddAuthentication();


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

app.UseSession();

app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<ChatHub>();
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
