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
    public class AddNewEmployee
    {
        private readonly EmployeeDbContext _empolyeeContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly Repository<Employee, long> _employeeRepository; 

        private readonly EmployeeBuilder employeeBuilder = new EmployeeBuilder();
        private readonly RoleBuilders jobroleBuilder = new RoleBuilders();
        public AddNewEmployee()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB")
                .Options;

            _empolyeeContext = new EmployeeDbContext(dbOptions);
            _unitOfWork = new UnitOfWork(_empolyeeContext);
            _employeeRepository = new Repository<Employee, long>(_unitOfWork);
        }

        [Fact]
        public void AddNewEmployeeToDB()
        {
            var employeeOne = employeeBuilder.BuildDefultEmployee();
            var employeeTwo = employeeBuilder.BuildDefultEmployee();
            employeeTwo.Email = "user2@gmail.com";
            var resultEmployee = _employeeRepository.Attach(employeeOne);
            var resultEmployee2 = _employeeRepository.Attach(employeeTwo);

            var result = _unitOfWork.Commit();
            Assert.Equal(2, result);
        }

        

    }
}
