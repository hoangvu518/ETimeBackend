// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Api.Models
{
    //public partial class Request
    //{
    //    public int Id { get; set; }
    //    public string RequestTitle { get; set; }
    //    public string RequestDescription { get; set; }
    //    public int RequestTypeId { get; set; }
    //    public bool IsApproved { get; set; }
    //    public int RequestedByEmployeeId { get; set; }

    //    public virtual RequestType RequestType { get; set; }
    //    public virtual Employee RequestedByEmployee { get; set; }
    //}

    public class Request
    {
        private Request()
        {
            //need for EF
        }
        public Request(string requestTitle, string requestDescription, int requestTypeId, int requestedBy)
        {
            RequestTitle = requestTitle;
            RequestDescription = requestDescription;
            RequestTypeId = requestTypeId;
            IsApproved = false;
            RequestedByEmployeeId = requestedBy;
        }

        public void Approve()
        {
            IsApproved = true;
        }
        public int Id { get; private set; }
        public string RequestTitle { get; private set; }
        public string RequestDescription { get; private set; }
        public int RequestTypeId { get; private set; }
        public bool IsApproved { get; private set; }
        public int RequestedByEmployeeId { get; private set; }

        public RequestType RequestType { get; private set; }
        public Employee RequestedByEmployee { get; private set; }
    }
}