using Api.Infrastructure;
using Api.Models;
using Api.Services.Dto;
using Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Exceptions;

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
            var emailAlreadyExists = await _dbContext.Employee.AnyAsync(x => x.Email == email);
            if (emailAlreadyExists)
            {
                throw new RecordDuplicateException($"Email already exists. Cannot add new employee with duplicate emails.");
            }

            var employee = new Employee(firstName, lastName, managerId, salary, email);
            await _dbContext.Employee.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateEmployeeManagerAsync(int employeeId, int? managerId)
        {
            if (!(managerId is null))
            {
                var manager = await _dbContext.Employee.FindAsync(managerId);
                if (manager is null)
                {
                    throw new RecordNotFoundException($"Invalid manager Id. Manager Id doesn't exist.");
                }
            }
            
            var employee = await _dbContext.Employee.FindAsync(employeeId);

            if (employee is null)
            {
                throw new RecordNotFoundException($"Invalid employee Id. Employee Id doesn't exist.");
            }

            employee.UpdateManager(managerId);
            //_dbContext.Attach(employee);
            //_dbContext.Entry(employee).Property(nameof(Employee.ManagerId)).IsModified = true;
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

        public async Task UpdateEmployeeAsync(int employeeId, string firstName, string lastName, string email)
        {
            var employee = await _dbContext.Employee.FindAsync(employeeId);
            if (employee is null)
            {
                throw new RecordNotFoundException($"Invalid employee id. Employee Id doesn't exist.");
            }

            employee.UpdateEmployeeInfo(firstName, lastName, email);
            await _dbContext.SaveChangesAsync();
            return;

        }
    }
}
