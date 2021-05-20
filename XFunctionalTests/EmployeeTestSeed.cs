using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XTestBuilders.EntitesBuilders;

namespace XFunctionalTests
{
    public class EmployeeTestSeed
    {
        private readonly static EmployeeBuilder employeeBuilder = new EmployeeBuilder();
        private readonly static RoleBuilders roleBuilder = new RoleBuilders();
        public async static Task Seed(EmployeeDbContext dbContext)
        {
            await SeedRoles(dbContext);
            await SeedEmployee(dbContext);
        }

        private async static Task SeedRoles(EmployeeDbContext dbContext)
        {
            if (!await dbContext.JobRoles.AnyAsync())
            {
                var roles = roleBuilder.BuildTestRoles();
                dbContext.JobRoles.AddRange(roles);
                dbContext.SaveChanges();
            }
        }
        
        private async static Task SeedEmployee(EmployeeDbContext dbContext)
        {
            if (!await dbContext.Employees.AnyAsync())
            {
                var employee = employeeBuilder.BuildListOfEmployees();
                dbContext.Employees.AddRange(employee);
                dbContext.SaveChanges();
            }
        }
        
       
    }
}
