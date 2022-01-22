using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.LeaveRequest
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISecurityService _securityService;
        private readonly IEmailService _emailService;
        public LeaveRequestController(IEmailService emailService, IEmployeeService employeeService, ISecurityService securityService)
        {
            _emailService = emailService;
            _employeeService = employeeService;
            _securityService = securityService;
        }
        [HttpGet("GetAllBy/{employeeName}")]
        public async Task<ActionResult<List<Request>>> GetAllBy(string employeeName)
        {
            var requests = await _employeeService.GetRequestsByEmployeeNameAsync(employeeName);
            if (requests.Count == 0)
            {
                return NotFound();
            }
            return Ok(requests);
        }


        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Request>> Get(int id)
        {
            var request = await _employeeService.GetRequestAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateRequestDto model)
        {
            var newRequest = await _employeeService.CreateRequestAsync(model.RequestTitle, model.RequestDescription, model.RequestTypeId, model.RequestedBy);
            if (newRequest == null)
            {
                return NotFound();
            }
            //get approverId from a service
            var approverId = 1111;
            _emailService.SendRequestForApproval(newRequest.Id, approverId);

            return CreatedAtAction(nameof(Get), new { id = newRequest.Id }, newRequest);
        }

        [HttpPost("Approve")]

        public async Task<ActionResult> Approve([FromQuery] int requestId)
        {
            //get userId from UserSecurity Service
            var approvingUserId = _securityService.GetUserId();

            var request = await _employeeService.GetRequestAsync(requestId);
            if (request is null)
            {
                return NotFound();
            }

            var requestedEmployeeId = request.RequestedByEmployeeId;


            var requestedEmployee = await _employeeService.GetEmployeeAsync(requestedEmployeeId);
            if (requestedEmployee is null)
            {
                return NotFound();
            }

            if (!requestedEmployee.IsManagedUnder(approvingUserId))
            {
                return Unauthorized("Error");
            }

            await _employeeService.ApproveRequestAsync(request.Id);
           
            return Ok();

        }
    }
}
