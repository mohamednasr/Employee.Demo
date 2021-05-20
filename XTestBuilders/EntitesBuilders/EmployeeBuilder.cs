using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace XTestBuilders.EntitesBuilders
{
    public class EmployeeBuilder
    {
        public Employee BuildDefultEmployee()
        {
            Employee employee = new Employee()
            {
                FirstName = "User",
                LastName = "1",
                Email = "User@test.com",
                Mobile = "01122334455",
                jobRoleId = 1
            };

            return employee;
        }

        public List<Employee> BuildListOfEmployees()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee(){FirstName = "User1", Email = "User1@admin.com", jobRoleId= 1},
                new Employee() { FirstName = "User2", Email = "User2@admin.com", jobRoleId = 2 },
                new Employee() { FirstName = "User3", Email = "User3@admin.com", jobRoleId = 2 },
                new Employee() { FirstName = "User4", Email = "User4@admin.com", jobRoleId = 1 }
            };
            return employees;
        }
    }
}
