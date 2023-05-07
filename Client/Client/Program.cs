using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Client.Services;
using Serilog;
using Client.IServices;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IGetUserService, GetUserService>();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    x =>
    {
        x.LoginPath = "/User/Login";
        x.AccessDeniedPath = "/Blog/Index";
    }
);
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();
app.UseStatusCodePagesWithReExecute("/Error/Error404", "?code={0}");
app.UseExceptionHandler("/Error/Error500");
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Blog}/{Action=Index}");
app.UseAuthentication();
app.UseAuthorization();
app.Run();