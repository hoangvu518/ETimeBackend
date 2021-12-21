using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.ResponseDto;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Lookup
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;
        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("GetAllRequestTypes")]
        public async Task<ActionResult<List<RequestTypeDto>>> GetAllRequestTypes()
        {
            var requestTypes = await _lookupService.GetAllRequestTypesAsync();
            return requestTypes;
        }

        [HttpPost("CreateRequestType")]
        public async Task<ActionResult> CreateRequestType(CreateRequestTypeDto model)
        {
            await _lookupService.CreateRequestTypeAsync(model.RequestTypeName);
            return Ok();

        }
    }
}
