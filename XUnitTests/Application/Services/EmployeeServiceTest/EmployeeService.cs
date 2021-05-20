using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using ApplicationCore.Services;
using XTestBuilders.EntitesBuilders;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace XUnitTests.Application.Services.EmployeeServiceTest
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IRepository<Employee, long>> _mockEmployeeRepo;
        private readonly Mock<IRepository<JobRole, int>> _mockJobRolesRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly EmployeeBuilder employeeBuilder = new EmployeeBuilder();
        public EmployeeServiceTest()
        {
            _mockEmployeeRepo = new Mock<IRepository<Employee, long>>();
            _mockJobRolesRepo = new Mock<IRepository<JobRole, int>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void GetAllEmployees()
        {
            var employeeServices = new EmployeeService(null, _mockEmployeeRepo.Object, _mockJobRolesRepo.Object, _mockUnitOfWork.Object) ;

            employeeServices.GetAllEmployees();

            _mockEmployeeRepo.Verify(x => x.GetAll(), Times.Once);

        }

        [Fact]
        public void GetEmployee()
        {
            var employeeServices = new EmployeeService(null, _mockEmployeeRepo.Object, _mockJobRolesRepo.Object, _mockUnitOfWork.Object);
            long id = 1;
            employeeServices.GetEmployee(id);

            _mockEmployeeRepo.Verify(x => x.GetFirstOrDefult(id), Times.Once);
        }

        [Fact]
        public void AddEmployee()
        {
            
            var employeeServices = new EmployeeService(null, _mockEmployeeRepo.Object, _mockJobRolesRepo.Object, _mockUnitOfWork.Object);

            employeeServices.AddEmployee(employeeBuilder.BuildDefultEmployee());

            _mockEmployeeRepo.Verify(x => x.Attach(It.IsAny<Employee>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public void UpdateEmployee()
        {
            
            var employeeServices = new EmployeeService(null, _mockEmployeeRepo.Object, _mockJobRolesRepo.Object, _mockUnitOfWork.Object);
           
            employeeServices.UpdateEmployee(employeeBuilder.BuildDefultEmployee());

            _mockEmployeeRepo.Verify(x => x.Update(It.IsAny<Employee>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public void DeleteEmployee()
        {
            Employee employeeOne = employeeBuilder.BuildDefultEmployee();
            var employeeServices = new EmployeeService(null, _mockEmployeeRepo.Object, _mockJobRolesRepo.Object, _mockUnitOfWork.Object);
            long employeeId = 1;
            var result = employeeServices.DeleteEmployee(employeeId);
            _mockEmployeeRepo.Verify(x => x.GetFirstOrDefult(It.IsAny<long>()), Times.Once);

            _mockEmployeeRepo.Verify(x => x.Delete(It.IsAny<Employee>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
            Assert.Equal(true, result.Result);

        }

       
    }
}
