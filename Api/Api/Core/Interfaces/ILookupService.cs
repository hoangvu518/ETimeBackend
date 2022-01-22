
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface ILookupService
    {
        Task<RequestType?> GetRequestType(int id);
        Task<List<RequestType>> GetAllRequestTypesAsync();
        Task<RequestType?> CreateRequestTypeAsync(string requestName);
    }
}
