using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Features.Employee;
using Api.Models;
using Api.Services.Dto;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISecurityService _securityService;
        public EmployeeController(IEmployeeService writeService, ISecurityService securityService)
        {
            _employeeService = writeService;
            _securityService = securityService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<List<EmployeeSimpleDto>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpPatch("UpdateManager")]
        public async Task<ActionResult> UpdateManager(UpdateManagerDto model)
        {
            //check for other security permissions here to prevent IDOR;
            await _employeeService.UpdateEmployeeManagerAsync(model.EmployeeId, model.ManagerId);
            return Ok();
        }

        [HttpPost("CreateEmployee")]
        public async Task<ActionResult> CreateEmployee(CreateEmployeeDto model)
        {
            //check for other security permissions here to prevent IDOR;
            await _employeeService.CreateEmployeeAsync(model.FirstName, model.LastName, model.ManagerId, model.Salary, model.Email);
            return Ok();
        }


    }
}
