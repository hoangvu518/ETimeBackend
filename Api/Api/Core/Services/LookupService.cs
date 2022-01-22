using Api.Infrastructure;
using Api.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Api.Exceptions;
using Api.Core.Interfaces;

namespace Api.Core.Services
{
    public class LookupService: ILookupService
    {
        private readonly TimeportalContext _dbContext;
        public LookupService(TimeportalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RequestType?> CreateRequestTypeAsync(string requestName)
        {
            var requestNameExists = await _dbContext.RequestType.AnyAsync(x => x.Name == requestName);
            if (requestNameExists)
            {
                throw new RecordDuplicateException("Invalid Request Name. Request Name already exists.");
            }

            var newRequestType = new RequestType(requestName);
            await _dbContext.RequestType.AddAsync(newRequestType);
            await _dbContext.SaveChangesAsync();
            return newRequestType;   
        }

        public async Task<List<RequestType>> GetAllRequestTypesAsync()
        {
            var requestTypes = await _dbContext.RequestType.ToListAsync();
            return requestTypes;
        }

        public async Task<RequestType?> GetRequestType(int id)
        {
            var requestType = await _dbContext.RequestType.FindAsync(id);
            return requestType;
        }
    }
}
