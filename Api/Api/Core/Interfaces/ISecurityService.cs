
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface ISecurityService
    {
        string GetFTBUserId();
        int GetUserId();
    }
}
