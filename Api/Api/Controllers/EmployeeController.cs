using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Interfaces;
using Api.Exceptions;
using Api.Models;
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
        private readonly IEmailService _emailService;
        public EmployeeController(IEmailService emailService, IEmployeeService writeService, ISecurityService securityService)
        {
            _emailService = emailService;
            _employeeService = writeService;
            _securityService = securityService;
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _employeeService.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Employee>>> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpPatch("UpdateManager")]
        public async Task<ActionResult> UpdateManager(UpdateManagerDto model)
        {
            //check for other security permissions here to prevent IDOR;
            await _employeeService.UpdateEmployeeManagerAsync(model.EmployeeId, model.ManagerId);
            return NoContent();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Employee>> Create(CreateEmployeeDto model)
        {
            //check for other security permissions here to prevent IDOR;
            var employee = await _employeeService.CreateEmployeeAsync(model.FirstName, model.LastName, model.ManagerId, model.Salary, model.Email);
            if (employee == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateEmployeeInfoDto model)
        {
            await _employeeService.UpdateEmployeeAsync(model.Id, model.FirstName, model.LastName, model.Email);
            return NoContent();
        }

    }
}
