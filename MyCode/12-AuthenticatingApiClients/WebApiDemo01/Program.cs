using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nito.AsyncEx;
using Serilog;
using Serilog.WxLibrary;
using WebApiDemo01.Models;

var serilogService = new SerilogService(SerilogService.DefaultOptions);
serilogService.Initialize();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddSerilog();

builder.Services.AddControllers();
builder.Services.AddDbContext<ProductDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:AppDataConnection"]);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 8;
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireNonAlphanumeric = false;
    opts.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<ProductDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
    {
        opts.TokenValidationParameters.ValidateAudience = false;
        opts.TokenValidationParameters.ValidateIssuer = false;
        opts.TokenValidationParameters.IssuerSigningKey
            = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["BearerTokens:Key"]));
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

AsyncContext.Run(async () => await ApplicationDbContextSeed.SeedData(app.Services));
app.Run();
