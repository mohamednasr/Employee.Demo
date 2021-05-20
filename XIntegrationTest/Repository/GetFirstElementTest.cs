using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using XTestBuilders.EntitesBuilders;
using Xunit;

namespace XIntegrationTest.Repository
{
    public class GetFirstElementTest
    {
        private readonly EmployeeDbContext _empolyeeContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly Repository<Employee, long> _employeeRepository; 

        private readonly EmployeeBuilder employeeBuilder = new EmployeeBuilder();
        private readonly RoleBuilders jobroleBuilder = new RoleBuilders();
        public GetFirstElementTest()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB")
                .Options;

            _empolyeeContext = new EmployeeDbContext(dbOptions);
            _unitOfWork = new UnitOfWork(_empolyeeContext);
            _employeeRepository = new Repository<Employee, long>(_unitOfWork);
        }

        [Fact]
        public void getFirstOrDefult()
        {
            var roleOne = jobroleBuilder.BuildDefultRole();
            _empolyeeContext.JobRoles.Add(roleOne);

            var employeeOne = employeeBuilder.BuildDefultEmployee();
            _empolyeeContext.Employees.Add(employeeOne);
            _empolyeeContext.SaveChanges();

            var resultEmployee = _employeeRepository.GetFirstOrDefult(1);

            Assert.Equal(1, resultEmployee.Id);
            Assert.Equal(employeeOne.FirstName, resultEmployee.FirstName);
            Assert.Equal("Role 1", resultEmployee.role.name);
        }

        [Fact]
        public void ReturnNullOnGetFirstOrDefult()
        {
            var employeeOne = employeeBuilder.BuildDefultEmployee();
            _empolyeeContext.Employees.Add(employeeOne);
            _empolyeeContext.SaveChanges();

            var resultEmployee = _employeeRepository.GetFirstOrDefult(10);

            Assert.Equal(null, resultEmployee);
        }
    }
}
