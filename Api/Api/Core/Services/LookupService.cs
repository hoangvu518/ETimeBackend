using Api.Infrastructure;
using Api.Models;
using Api.Services.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.ResponseDto;
using System.ComponentModel.DataAnnotations;
using Api.Exceptions;

namespace Api.Services
{
    public class LookupService: ILookupService
    {
        private readonly TimeportalContext _dbContext;
        public LookupService(TimeportalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateRequestTypeAsync(string requestName)
        {
            var requestNameExists = await _dbContext.RequestType.AnyAsync(x => x.Name == requestName);
            if (requestNameExists)
            {
                throw new RecordDuplicateException("Invalid Request Name. Request Name already exists.");
            }

            var newRequestType = new RequestType(requestName);
            await _dbContext.RequestType.AddAsync(newRequestType);
            await _dbContext.SaveChangesAsync();
            
        }

        public async Task<List<RequestTypeDto>> GetAllRequestTypesAsync()
        {
            var requestTypes = await _dbContext.RequestType.Select(x => new RequestTypeDto { Id = x.Id, Name = x.Name })
                                                            .ToListAsync();
            return requestTypes;
        }
    }
}
