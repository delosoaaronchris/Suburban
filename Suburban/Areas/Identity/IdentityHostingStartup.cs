using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suburban.Areas.Identity.Data;
using Suburban.Data;

[assembly: HostingStartup(typeof(Suburban.Areas.Identity.IdentityHostingStartup))]
namespace Suburban.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SuburbanDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SuburbanDbContextConnection")));

                services.AddDefaultIdentity<SuburbanUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                      .AddRoles<IdentityRole>()
                     .AddEntityFrameworkStores<SuburbanDbContext>();
            });
        }
    }
}