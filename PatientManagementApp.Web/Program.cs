using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


using PatientManagementApp.Data;
using PatientManagementApp.Data.Models;

using PatientManagementApp.Services.Data.Interfaces;
using PatientManagementApp.Services.Mapping;
using PatientManagementApp.Web.Infrastructure.Extensions;
using PatientManagementApp.Web.ViewModels;

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
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure()));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/Identity/Account/Login";
                cfg.LogoutPath = "/Identity/Account/Logout";
            });


            builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(IAppointmentService).Assembly);

            // Register Database Seeder
            builder.Services.AddScoped<DatabaseSeeder>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

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
                var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
                await context.Database.MigrateAsync(); // Replaces EnsureCreatedAsync

                await seeder.SeedAsync();
            }


            //using (var scope = app.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //    // Ensure the database is created (necessary if it hasn't been initialized yet)
            //    await context.Database.EnsureCreatedAsync();


            //    await context.SeedData(scope.ServiceProvider);
            //}

            app.Run();
        }
    }
}
