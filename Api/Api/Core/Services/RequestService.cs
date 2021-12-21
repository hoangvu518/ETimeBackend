using Api.Infrastructure;
using Api.Models;
using Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class RequestService : IRequestService
    {
        private readonly TimeportalContext _dbContext;
        public RequestService(TimeportalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ApproveRequestAsync(int requestId)
        {
            var request = await _dbContext.Request.FindAsync(requestId);

            if (request is null)
            {
                throw new ArgumentException("Invalid reqest Id");
            }

            request.Approve();
            //_dbContext.Attach(request);
            //_dbContext.Entry(request).Property(nameof(Request.IsApproved)).IsModified = true;
            await _dbContext.SaveChangesAsync();

        }

        public async Task<Request> CreateRequestAsync(string requestTitle, string requestDescription, int requestTypeId, int requestedBy)
        {
            var requestTypeExits = await _dbContext.RequestType.AnyAsync(x=> x.Id == requestTypeId);
            if (!requestTypeExits)
            {
                throw new ArgumentException("Invalid request type Id");
            }

            var requestEmployeeExists = await _dbContext.Employee.AnyAsync(x=> x.Id == requestedBy);

            if (!requestEmployeeExists)
            {
                throw new ArgumentException("Invalid employee Id");
            }

            var unapprovedRequestCount = await _dbContext.Request.CountAsync(x => x.RequestedByEmployeeId == requestedBy && x.IsApproved == false);
            if (unapprovedRequestCount >= 5)
            {
                throw new Exception("An employee can have a maximum of 5 unapproved requests.");
            }

            var newRequest = new Request(requestTitle, requestDescription, requestTypeId, requestedBy);
            await _dbContext.Request.AddAsync(newRequest);
            await _dbContext.SaveChangesAsync();

            return newRequest;
        }

        public async Task<Request> GetRequestAsync(int requestId)
        {
            var request = await _dbContext.Request.FindAsync(requestId);
            return request;
        }
    }
}
