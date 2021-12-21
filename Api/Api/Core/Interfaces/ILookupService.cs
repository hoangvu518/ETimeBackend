using Api.Core.ResponseDto;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface ILookupService
    {
        Task<List<RequestTypeDto>> GetAllRequestTypesAsync();
        Task CreateRequestTypeAsync(string requestName);
    }
}
