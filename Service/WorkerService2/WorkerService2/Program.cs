//using Microsoft.Extensions.Configuration;
using WorkerService2;
using WorkerService2.Service;
using WorkerService2.Service.IService;
//using Microsoft.Extensions.DependencyInjection;
using WorkerService2.DBContext;
using WorkerService2.Utility;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();


builder.Services.AddHttpClient();
//builder.Services.AddHttpContextAccessor();

// Add the `IConfiguration` implementation
//builder.Services.AddSingleton<IConfiguration>(Configuration);

// Register the SQL Server database connection string
//builder.Services.AddSingleton<string>(provider => provider.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")??"");



//bdi
SD.OACClientID = builder.Configuration["DanamonAuthData:OACClientID"];
SD.OACClientIDSecret = builder.Configuration["DanamonAuthData:OACClientIDSecret"];
SD.BDIKey = builder.Configuration["DanamonAuthData:BDIKey"];
SD.BDIKeySecret = builder.Configuration["DanamonAuthData:BDIKeySecret"];
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
//SD.RSAKey = builder.Configuration["RSABDI:RSAKeyAuth"];
//bdi


//proxy
SD.JktProxy = builder.Configuration["ServiceUrls:JktProxy"];
SD.JktPort = int.Parse(builder.Configuration["ServiceUrls:JktPort"] ?? "0");
//proxy

builder.Services.AddSingleton<DapperContext>();

// Register the `ICompanyRepository` implementation
builder.Services.AddSingleton<ICompany, Company>();

// Register the background service
builder.Services.AddHostedService<Worker>();

builder.Services.AddHostedService<CompanyBackgroundService>();



var host = builder.Build();
host.Run();
