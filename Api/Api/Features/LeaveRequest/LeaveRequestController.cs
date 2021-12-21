using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ExternalServices.Interfaces;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.LeaveRequest
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IEmployeeService _employeeService;
        private readonly ISecurityService _securityService;
        private readonly IEmailService _emailService;
        public LeaveRequestController(IRequestService requestService, IEmailService emailService, IEmployeeService employeeService, ISecurityService securityService)
        {
            _requestService = requestService;
            _emailService = emailService;
            _employeeService = employeeService;
            _securityService = securityService;
        }

        [HttpPost("CreateRequest")]
        public async Task<ActionResult> CreateRequest(CreateRequestDto model)
        {
            var newRequest = await _requestService.CreateRequestAsync(model.RequestTitle, model.RequestDescription, model.RequestTypeId, model.RequestedBy);
            //get approverId from a service
            var approverId = 1111;
            _emailService.SendRequestForApproval(newRequest.Id, approverId);

            return Ok();
        }

        [HttpPost("ApproveRequest")]

        public async Task<ActionResult> ApproveRequest([FromQuery] int requestId)
        {
            //get userId from UserSecurity Service
            var approvingUserId = _securityService.GetUserId();

            var request = await _requestService.GetRequestAsync(requestId);
            if (request is null)
            {
                return BadRequest();
            }

            var requestedEmployeeId = request.RequestedByEmployeeId;


            var requestedEmployee = await _employeeService.GetEmployeeAsync(requestedEmployeeId);
            if (requestedEmployee is null)
            {
                return BadRequest();
            }

            if (!requestedEmployee.IsManagedUnder(approvingUserId))
            {
                return Unauthorized("Error");
            }

            await _requestService.ApproveRequestAsync(request.Id);
           
            return Ok();

        }
    }
}
