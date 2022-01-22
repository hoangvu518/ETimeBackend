using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee?> CreateEmployeeAsync(string firstName, string lastName, int? managerId, decimal? salary, string email);
        Task UpdateEmployeeAsync(int employeeId, string firstName, string lastName, string email);
        Task UpdateEmployeeManagerAsync(int employeeId, int? managerId);
        Task<Employee?> GetEmployeeAsync(int employeeId);
        Task<List<Employee>> GetAllEmployeesAsync();

        Task<Request?> CreateRequestAsync(string requestTitle, string requestDescription, int requestTypeId, int requestedBy);
        Task<Request?> GetRequestAsync(int requestId);
        Task<List<Request>> GetRequestsByEmployeeNameAsync(string employeeName);
        Task ApproveRequestAsync(int requestId);

    }
}
