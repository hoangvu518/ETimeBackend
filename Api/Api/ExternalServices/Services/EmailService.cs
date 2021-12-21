using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ExternalServices.Interfaces
{
    public class EmailService : IEmailService
    {
        public void SendRequestForApproval(int requestId, int approverId)
        {
            //implement sending email
            Console.WriteLine("Email sent.");
        }
    }
}
