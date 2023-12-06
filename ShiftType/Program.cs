using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using ShiftType.DbModels;
using ShiftType.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Services
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TypingDbContext>(options =>
{
    SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
    options.UseSqlite("Data Source=D:\\Mein progectos\\ShiftType\\ShiftType\\TypingDb.db");
});

//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<TypingDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddAuthentication();
    //.AddGoogle(options =>
    //{
    //    options.ClientId = "YourClientId";
    //    options.ClientSecret = "YourClientSecret";
    //});
builder.Services.AddControllersWithViews()
    .AddViewLocalization();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});
// Configure
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//var supportedCultures = new[]
//{
//    new CultureInfo("en-US"),
//    new CultureInfo("uk-UA"),
//};
//var localizationOptions = new RequestLocalizationOptions
//{
//    DefaultRequestCulture = new RequestCulture("en-US"),
//    SupportedCultures = supportedCultures,
//    SupportedUICultures = supportedCultures
//};
//app.UseRequestLocalization(localizationOptions);

//app.Use(async (context, next) =>
//{
//    var selectedLanguage = context.Request.Cookies["SelectedLanguage"];
//    if (!string.IsNullOrEmpty(selectedLanguage))
//    {
//        CultureInfo.CurrentCulture = new CultureInfo(selectedLanguage);
//        CultureInfo.CurrentUICulture = new CultureInfo(selectedLanguage);
//    }

//    await next();
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Type}/{action=Index}");

app.Run();
