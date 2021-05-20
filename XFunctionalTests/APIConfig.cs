using API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Data;
using ApplicationCore.Interfaces;

namespace XFunctionalTests
{
    public class APIConfig: WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                services.AddEntityFrameworkInMemoryDatabase();

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<EmployeeDbContext>(options =>
                {
                    options.UseInMemoryDatabase("EmployeeDemoTesting");
                    options.UseInternalServiceProvider(provider);
                });

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("Identity");
                    options.UseInternalServiceProvider(provider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<EmployeeDbContext>();

                    var loggerService = scopedServices.GetRequiredService<IloggerService>();

                    var dbCreated = db.Database.EnsureCreated();

                    try
                    {
                        EmployeeTestSeed.Seed(db).Wait();
                        var userManager = scopedServices.GetRequiredService<UserManager<IdentityUser>>();
                        var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                        SeedData.Seed(userManager, roleManager).Wait();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            });
        }
    }
}
