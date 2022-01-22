using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class FullRequestInfoDto
    {
        public int Id { get;  set; }
        public string RequestTitle { get; set; } = string.Empty;
        public string RequestDescription { get; set; } = string.Empty;
        public int RequestTypeId { get; set; } 
        public string RequestTypeName { get; set; } = string.Empty;
        public bool IsApproved { get; set; } 
        public int RequestedByEmployeeId { get;  set; }

        public string RequestedEmployeeLastName { get; set; } = string.Empty;
        public string RequestedEmployeeFirstName { get; set; } = string.Empty;
    }
}
