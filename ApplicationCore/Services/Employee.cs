using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IloggerService _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Employee, long> _employeeRepo;
        private readonly IRepository<JobRole, int> _jobRoles;
        public EmployeeService(IloggerService logger, IRepository<Employee, long> employeeRepo, IRepository<JobRole, int> jobRoles, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _employeeRepo = employeeRepo;
            _jobRoles = jobRoles;
        }
        public DescriptiveResponse<bool> AddEmployee(Employee employee)
        {
            try
            {
                //JobRole role = _jobRoles.GetFirstOrDefult(employee.role.Id);
                //employee.role = role;
                var result = _employeeRepo.Attach(employee);
                var saved = _unitOfWork.Commit();
                return new DescriptiveResponse<bool>().success(true);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return new DescriptiveResponse<bool>().Error(ex.Message);

            }
        }

        public DescriptiveResponse<bool> DeleteEmployee(long employeeId)
        {
            try
            {
                Employee _employee = _employeeRepo.GetFirstOrDefult(employeeId);
                var result = _employeeRepo.Delete(_employee);
                var saved = _unitOfWork.Commit();
                if(saved > 0)
                    return new DescriptiveResponse<bool>().success(true);
                return new DescriptiveResponse<bool>().Error("Something went wrong");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new DescriptiveResponse<bool>().Error(ex.Message);
            }
        }

        public DescriptiveResponse<List<Employee>> SearchEmployee(string search)
        {
            try
            {
                string[] splitedSearch = search.TrimStart().Split(' ');
                List<Employee> employees = _employeeRepo.Get((o => search.Contains(o.FirstName) || search.Contains(o.LastName) || search.Contains(o.Email))).ToList();
                if (employees.Count > 0)
                {
                    return new DescriptiveResponse<List<Employee>>().success(employees);
                }
                else
                {
                    return new DescriptiveResponse<List<Employee>>().NotFound();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new DescriptiveResponse<List<Employee>>().Error(ex.Message);
            }
        }

        public DescriptiveResponse<List<Employee>> GetAllEmployees()
        {
            try
            {
                List<Employee> employees = _employeeRepo.GetAll().ToList();
                if (employees.Count > 0)
                    return new DescriptiveResponse<List<Employee>>().success(employees);
                else
                    return new DescriptiveResponse<List<Employee>>().success(new List<Employee>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new DescriptiveResponse<List<Employee>>().Error(ex.Message);
            }
        }

        public DescriptiveResponse<Employee> GetEmployee(long id)
        {
            try
            {
                Employee employee = _employeeRepo.GetFirstOrDefult(id);
                if (employee != null)
                    return new DescriptiveResponse<Employee>().success(employee);
                else
                    return new DescriptiveResponse<Employee>().NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new DescriptiveResponse<Employee>().Error(ex.Message);
            }
        }

        
        public DescriptiveResponse<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                //JobRole role = _jobRoles.GetFirstOrDefult(employee.role.Id);
                //employee.role = role;
                
                var result = _employeeRepo.Update(employee);
                
               var saved = _unitOfWork.Commit();
               
                return new DescriptiveResponse<bool>().success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new DescriptiveResponse<bool>().Error(ex.Message);
            }
        }
    }
}
