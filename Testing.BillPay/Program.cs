using Testing.BillPay.Service;
using Testing.BillPay.Service.IService;
using Testing.BillPay.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
//SD.RSAKey = builder.Configuration["RSABDI:RSAKeyAuth"];
//bdi


builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDanamonService, DanamonService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
