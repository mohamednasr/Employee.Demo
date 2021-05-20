using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Logger;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.EmployeeEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
        [Authorize]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DescriptiveResponse<List<Employee>> getEmployees()
        {
            return _employeeService.GetAllEmployees();
        }
        
        /// <summary>
        /// Get specific Employee By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public DescriptiveResponse<Employee> getEmployee(long id)
        {
            return _employeeService.GetEmployee(id);
        }
        
        /// <summary>
        /// Search for Employee by query string
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("${search}")]
        public DescriptiveResponse<List<Employee>> getEmployee(string search)
        {
            return _employeeService.SearchEmployee(search);
        }


        [HttpPost]
        public DescriptiveResponse<bool> addEmployee(Employee employee)
        {
            return _employeeService.AddEmployee(employee);
        }

        [HttpPut]
        public DescriptiveResponse<bool> updateEmployee(Employee employee)
        {
            return _employeeService.UpdateEmployee(employee);
        }

        [HttpDelete]
        public DescriptiveResponse<bool> deleteEmployee(long employeeId)
        {
            return _employeeService.DeleteEmployee(employeeId);
        }
    }
}
