using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.ResponseDto
{
    public class FullRequestInfoDto
    {
        public int Id { get;  set; }
        public string RequestTitle { get;  set; }
        public string RequestDescription { get;  set; }
        public int RequestTypeId { get;  set; }
        public string RequestTypeName { get; set; }
        public bool IsApproved { get;  set; }
        public int RequestedByEmployeeId { get;  set; }

        public string RequestedEmployeeLastName { get; set; }
        public string RequestedEmployeeFirstName { get; set; }
    }
}
