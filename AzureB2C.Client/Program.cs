using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = "https://sideriantest.b2clogin.com/sideriantest.onmicrosoft.com/B2C_1_sideriantest/v2.0/";
    options.ClientId = "c6f506bf-d156-4b42-a2d3-aeb84046d07e";
    options.ResponseType = "Code";
    options.SaveTokens = true;
    //options.Scope.Add("cb86c1d0-996d-4800-81f7-7b9ecae78677");
    options.Scope.Add("https://sideriantest.onmicrosoft.com/api/FullAccess");
    options.ClientSecret = "6RG8Q~GhPtmn5x2zM5n3Fo2c4bZNbG_7mH-J-bB.";
    //options.AccessDeniedPath = "/Home/ResetPassword";
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();