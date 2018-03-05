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
                   .AddDefaultUI()
                   .AddDefaultTokenProviders();
            });
        }
    }
}