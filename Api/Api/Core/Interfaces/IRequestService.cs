﻿using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IRequestService
    {
        Task<Request> CreateRequestAsync(string requestTitle, string requestDescription, int requestTypeId, int requestedBy);
        Task<Request> GetRequestAsync(int requestId);
        Task ApproveRequestAsync(int requestId);
    }
}