using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Testing.DanamonNew.Service;
using Testing.DanamonNew.Service.IService;
using Testing.DanamonNew.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//custom service to the container
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
//sutom again

builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<IDanamonService, DanamonService>();

SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
SD.RegisterVaAPIBase = builder.Configuration["ServiceUrls:RegisterVaAPI"];

//bdi
SD.OACClientID = builder.Configuration["DanamonAuthData:OACClientID"];
SD.OACClientIDSecret = builder.Configuration["DanamonAuthData:OACClientIDSecret"];
SD.BDIKey = builder.Configuration["DanamonAuthData:BDIKey"];
SD.BDIKeySecret = builder.Configuration["DanamonAuthData:BDIKeySecret"];
//bdi


builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDanamonService, DanamonService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Home/GetAuthorize";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

//end custom


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
