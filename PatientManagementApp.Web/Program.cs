using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using PatientManagementApp.Data;
using PatientManagementApp.Data.Models;
using PatientManagementApp.Web.Data;

namespace PatientManagementApp.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("SQLServer") ?? throw new InvalidOperationException("Connection string 'SQLServer' not found.");
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //Identity
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
            app.MapRazorPages();


            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Ensure the database is created (necessary if it hasn't been initialized yet)
                await context.Database.EnsureCreatedAsync();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                ////seeding roles 
                //var roles = new[] { "Admin", "User", "Client" };

                //foreach (var role in roles)
                //{
                //    if (!await roleManager.RoleExistsAsync(role))
                //    {
                //        await roleManager.CreateAsync(new IdentityRole(role));
                //    }
                //}

                await context.SeedData(scope.ServiceProvider);
            }


            //using (var scope = app.Services.CreateScope())
            //{
            //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //    string email = "admin@admin.com";
            //    string password = "Admin123!";

            //    if (await userManager.FindByEmailAsync(email) == null)
            //    {
            //        var user = new ApplicationUser
            //        {
            //            Email = email,
            //            UserName = email
            //        };

            //        await userManager.CreateAsync(user, password);

            //        //assign a role to the account
            //        await userManager.AddToRoleAsync(user, "Admin");
            //    }
            //}

            app.Run();
        }
    }
}
