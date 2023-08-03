using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using YourLibrary.Abstractions;
using YourLibrary.Data;
using YourLibrary.Helpers;
using YourLibrary.Models;
using YourLibrary.Repository;
using YourLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultString");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IRepository<Author>, AuthorRepository>();
builder.Services.AddScoped<IRepository<Image>, ImageRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection(CloudinarySettings.CloudinarySettingsSectionName)
);

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages();

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


//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "Create",
    pattern: "{controller=Books}/{action=Create}/{id?}"
);

app.MapControllerRoute(
    name: "Edit",
    pattern: "{controller=Books}/{action=Edit}/{id?}"
);

app.Run();
