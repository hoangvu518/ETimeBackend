using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ExternalServices.Interfaces
{
    public interface IEmailService
    {
        void SendRequestForApproval(int requestId, int approverId);

    }
}
