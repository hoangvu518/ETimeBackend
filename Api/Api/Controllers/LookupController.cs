using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Interfaces;
using Api.Models;
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
        [HttpGet("GetRequest/{id}")]
        public async Task<ActionResult<RequestType>> GetRequestType(int id)
        {
            var requestType = await _lookupService.GetRequestType(id);
            if (requestType == null)
            {
                return NotFound();
            }
            return Ok(requestType);
        }

        [HttpGet("GetAllRequestTypes")]
        public async Task<ActionResult<List<RequestType>>> GetAllRequestTypes()
        {
            var requestTypes = await _lookupService.GetAllRequestTypesAsync();
            if (requestTypes.Count == 0)
            {
                return NotFound();
            }
            return requestTypes;
        }

        [HttpPost("CreateRequestType")]
        public async Task<ActionResult<RequestType>> CreateRequestType(CreateRequestTypeDto model)
        {
            var requestType =  await _lookupService.CreateRequestTypeAsync(model.RequestTypeName);
            if (requestType == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRequestType), new { id = requestType.Id }, requestType);

        }
    }
}
