using Api.Models;
using Api.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task CreateEmployeeAsync(string firstName, string lastName, int? managerId, decimal? salary, string email);
        Task UpdateEmployeeManagerAsync(int employeeId, int? managerId);

        Task<List<EmployeeSimpleDto>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int employeeId);

    }
}
