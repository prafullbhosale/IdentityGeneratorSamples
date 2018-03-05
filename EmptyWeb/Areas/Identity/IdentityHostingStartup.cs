using System;
using EmptyWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EmptyWeb.Areas.Identity.IdentityHostingStartup))]
namespace EmptyWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<EmptyWebIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("EmptyWebIdentityDbContextConnection"),
                        sqlOptions => sqlOptions.MigrationsAssembly("EmptyWeb")));

                services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<EmptyWebIdentityDbContext>()
                   .AddDefaultTokenProviders();
                services.AddMvc()
                    .AddRazorPagesOptions(options =>
                    {
                        options.AllowAreas = true;
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                    });

                services.ConfigureApplicationCookie(options => 
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                });
            });
        }
    }
}