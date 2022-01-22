using Api.Infrastructure;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Exceptions;
using Api.Core.Interfaces;

namespace Api.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly TimeportalContext _dbContext;
        public EmployeeService(TimeportalContext dbContext)
        {
            _dbContext = dbContext;
        }
 
        public async Task<Employee?> GetEmployeeAsync(int employeeId)
        {
            var employee = await _dbContext.Employee.FindAsync(employeeId);
            return employee;
                                                    
        }

        public async Task<Employee?> CreateEmployeeAsync(string firstName, string lastName, int? managerId, decimal? salary, string email)
        {
            var emailAlreadyExists = await _dbContext.Employee.AnyAsync(x => x.Email == email);
            if (emailAlreadyExists)
            {
                throw new RecordDuplicateException($"Email already exists. Cannot add new employee with duplicate emails.");
            }
            var employee = new Employee(firstName, lastName, managerId, salary, email);
            await _dbContext.Employee.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;

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

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = await _dbContext.Employee.ToListAsync();
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


        public async Task ApproveRequestAsync(int requestId)
        {
            var request = await _dbContext.Request.FindAsync(requestId);

            if (request is null)
            {
                throw new RecordNotFoundException("Invalid reqest Id");
            }

            request.Approve();
            //_dbContext.Attach(request);
            //_dbContext.Entry(request).Property(nameof(Request.IsApproved)).IsModified = true;
            await _dbContext.SaveChangesAsync();

        }

        public async Task<Request?> CreateRequestAsync(string requestTitle, string requestDescription, int requestTypeId, int requestedBy)
        {
            var requestTypeExits = await _dbContext.RequestType.AnyAsync(x => x.Id == requestTypeId);
            if (!requestTypeExits)
            {
                throw new RecordNotFoundException("Invalid request type Id");
            }

            var requestEmployeeExists = await _dbContext.Employee.AnyAsync(x => x.Id == requestedBy);

            if (!requestEmployeeExists)
            {
                throw new RecordNotFoundException("Invalid employee Id");
            }

            var unapprovedRequestCount = await _dbContext.Request.CountAsync(x => x.RequestedByEmployeeId == requestedBy && x.IsApproved == false);
            if (unapprovedRequestCount >= 5)
            {
                throw new BusinessLogicException("An employee can have a maximum of 5 unapproved requests.");
            }

            var newRequest = new Request(requestTitle, requestDescription, requestTypeId, requestedBy);
            await _dbContext.Request.AddAsync(newRequest);
            await _dbContext.SaveChangesAsync();

            return newRequest;
        }

        public Task<List<Request>> GetRequestsByEmployeeNameAsync(string employeeName)
        {
            var request = _dbContext.Request.Where(x => x.RequestedByEmployee.FirstName == employeeName || x.RequestedByEmployee.LastName == employeeName)
                                            .ToListAsync();
            return request;
        }

        public async Task<Request?> GetRequestAsync(int requestId)
        {
            var request = await _dbContext.Request.FindAsync(requestId);
            return request;
        }
    }
}
