using Api.Infrastructure;
using Api.Models;
using Api.Services.Dto;
using Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly TimeportalContext _dbContext;
        public EmployeeService(TimeportalContext dbContext)
        {
            _dbContext = dbContext;
        }
 
        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            var employee = await _dbContext.Employee.FindAsync(employeeId);
            return employee;
                                                    
        }

        public async Task CreateEmployeeAsync(string firstName, string lastName, int? managerId, decimal? salary, string email)
        {
            var emailAlreadyExists = _dbContext.Employee.Any(x => x.Email == email);
            if (emailAlreadyExists)
            {
                throw new ArgumentException($"Email already exists. Cannot add new employee with duplicate emails");
            }

            var employee = new Employee(firstName, lastName, managerId, salary, email);
            _dbContext.Employee.Add(employee);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateEmployeeManagerAsync(int employeeId, int? managerId)
        {
            if (!(managerId is null))
            {
                var manager = _dbContext.Employee.Find(managerId);
                if (manager is null)
                {
                    throw new ArgumentException($"Invalid manager Id");
                }
            }
            
            var employee = _dbContext.Employee.Find(employeeId);

            if (employee is null)
            {
                throw new ArgumentException($"Invalid employee Id");
            }

            employee.UpdateManager(managerId);
            _dbContext.Attach(employee);
            _dbContext.Entry(employee).Property(nameof(Employee.ManagerId)).IsModified = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EmployeeSimpleDto>> GetAllEmployeesAsync()
        {
            var employees = await _dbContext.Employee.Select(x => new EmployeeSimpleDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }).ToListAsync();

            return employees;

        }
    }
}
