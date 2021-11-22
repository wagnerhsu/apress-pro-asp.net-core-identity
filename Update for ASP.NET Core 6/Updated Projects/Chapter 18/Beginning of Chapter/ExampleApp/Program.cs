using ExampleApp.Custom;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ExampleApp.Identity;
using ExampleApp.Identity.Store;
using ExampleApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ILookupNormalizer, Normalizer>();
builder.Services.AddSingleton<IUserStore<AppUser>, UserStore>();
builder.Services.AddIdentityCore<AppUser>();
builder.Services.AddSingleton<IUserValidator<AppUser>, EmailValidator>();
builder.Services.AddSingleton<IEmailSender, ConsoleEmailSender>();
builder.Services.AddSingleton<ISMSSender, ConsoleSMSSender>();

builder.Services.AddIdentityCore<AppUser>(opts => {
    opts.Tokens.EmailConfirmationTokenProvider = "SimpleEmail";
    opts.Tokens.ChangeEmailTokenProvider = "SimpleEmail";
})
.AddTokenProvider<EmailConfirmationTokenGenerator>("SimpleEmail")
.AddTokenProvider<PhoneConfirmationTokenGenerator>(
     TokenOptions.DefaultPhoneProvider);


builder.Services.AddAuthentication(opts => {
    opts.DefaultScheme
        = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(opts => {
    opts.LoginPath = "/signin";
    opts.AccessDeniedPath = "/signin/403";
});
builder.Services.AddAuthorization(opts => {
    AuthorizationPolicies.AddPolicies(opts);
});
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();

app.UseMiddleware<RoleMemberships>();
app.UseAuthorization();

app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.MapFallbackToPage("/Secret");

app.Run();
