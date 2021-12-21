
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface ISecurityService
    {
        string GetFTBUserId();
        int GetUserId();
    }
}
