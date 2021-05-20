using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IEmployeeService
    {
        public DescriptiveResponse<Employee> GetEmployee(long id);
        public DescriptiveResponse<List<Employee>> SearchEmployee(string search);
        public DescriptiveResponse<List<Employee>> GetAllEmployees();
        public DescriptiveResponse<bool> AddEmployee(Employee employee);
        public DescriptiveResponse<bool> UpdateEmployee(Employee employee);
        public DescriptiveResponse<bool> DeleteEmployee(long employee);

    }
}
