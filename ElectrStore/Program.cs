using System.Drawing.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ElectrStore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("StoreContext");
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(
        options =>
            options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<StoreContext>();
builder.Services.AddRazorPages();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    SeedRoles.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

public static class SeedRoles
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new StoreContext(serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>()))
        {
            string[] roles = new string[] { "Owner", "Administrator", "User" };
            var newRoleList = new List<IdentityRole>();
            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    newRoleList.Add(new IdentityRole(role));
                }
            }

            context.Roles.AddRange(newRoleList);
            context.SaveChanges();
        }
    }
}