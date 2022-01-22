using Api.Core.Interfaces;
using Api.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services
{
    //Deal with roles and stuff
    public class SecurityService : ISecurityService
    {
        private readonly TimeportalContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SecurityService(TimeportalContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetFTBUserId()
        {
            //insert code to get user id from http context;
            return "F2051";
        }

        public int GetUserId()
        {
            return 4;
        }


    }
}
