using Api.Infrastructure;
using Api.Models;
using Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Core.ResponseDto;
using Api.Exceptions;

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
                throw new RecordNotFoundException("Invalid reqest Id");
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
                throw new RecordNotFoundException("Invalid request type Id");
            }

            var requestEmployeeExists = await _dbContext.Employee.AnyAsync(x=> x.Id == requestedBy);

            if (!requestEmployeeExists)
            {
                throw new RecordNotFoundException("Invalid employee Id");
            }

            var unapprovedRequestCount = await _dbContext.Request.CountAsync(x => x.RequestedByEmployeeId == requestedBy && x.IsApproved == false);
            if (unapprovedRequestCount >= 5)
            {
                throw new BusinessLogicException("An employee can have a maximum of 5 unapproved requests.");
            }

            var newRequest = new Request(requestTitle, requestDescription, requestTypeId, requestedBy);
            await _dbContext.Request.AddAsync(newRequest);
            await _dbContext.SaveChangesAsync();

            return newRequest;
        }

        public Task<List<FullRequestInfoDto>> GetFullRequestInfoByEmployeeAsync(string employeeName)
        {
            var request = _dbContext.Request.Where(x => x.RequestedByEmployee.FirstName == employeeName || x.RequestedByEmployee.LastName == employeeName)
                                            .Include(x => x.RequestedByEmployee)
                                            .Include(x => x.RequestType)
                                            .Select(x => new FullRequestInfoDto
                                            {
                                                Id = x.Id,
                                                RequestTitle = x.RequestTitle,
                                                RequestDescription = x.RequestDescription,
                                                IsApproved = x.IsApproved,
                                                RequestedByEmployeeId = x.RequestedByEmployeeId,
                                                RequestedEmployeeFirstName = x.RequestedByEmployee.FirstName,
                                                RequestedEmployeeLastName = x.RequestedByEmployee.LastName,
                                                RequestTypeId = x.RequestTypeId,
                                                RequestTypeName = x.RequestType.Name
                                            })
                                            .ToListAsync();
            return request;
        }

        public async Task<Request> GetRequestAsync(int requestId)
        {
            var request = await _dbContext.Request.FindAsync(requestId);
            return request;
        }
    }
}
