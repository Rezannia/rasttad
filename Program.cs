using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rasttad.Data;

var builder = WebApplication.CreateBuilder(args);

// لاگ کردن DATABASE_URL برای دیباگ
var dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
Console.WriteLine($"=== DATABASE_URL: {dbUrl ?? "NULL"} ===");

// تنظیم پورت برای Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllersWithViews();

// اتصال به دیتابیس
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// اگر متغیر محیطی DATABASE_URL وجود داشت (روی Render)، از آن استفاده کن
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(databaseUrl))
{
    // Render فرمت postgres:// می‌دهد که Uri نمی‌تواند درست تجزیه کند
    // پس به صورت دستی تجزیه می‌کنیم
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var host = uri.Host;
    var port = uri.Port > 0 ? uri.Port : 5432; // پورت پیش‌فرض PostgreSQL
    var database = uri.AbsolutePath.TrimStart('/');
    var username = userInfo[0];
    var password = userInfo.Length > 1 ? userInfo[1] : "";

    connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// اضافه کردن Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// اضافه کردن داده‌های اولیه + ساخت کاربر ادمین
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // اعمال Migrationها به صورت خودکار
    await context.Database.MigrateAsync();

    DbSeeder.Seed(context);
    await SeedAdminAsync(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/Home/Error404");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

// متد ساخت کاربر ادمین
static async Task SeedAdminAsync(IServiceProvider services)
{
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminEmail = "admin@rasttad.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(adminUser, "Admin@123");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}