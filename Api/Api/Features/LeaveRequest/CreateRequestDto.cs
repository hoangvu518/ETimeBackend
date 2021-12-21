using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Features.LeaveRequest
{
    public class CreateRequestDto
    {
        public string RequestTitle { get; set; }
        public string RequestDescription { get; set; }
        public int RequestTypeId { get; set; }

        public int RequestedBy { get; set; }
    }

    public class CreateRequestDtoValidator : AbstractValidator<CreateRequestDto>
    {
        public CreateRequestDtoValidator()
        {
            RuleFor(x => x.RequestTitle).NotEmpty()
                                    .MaximumLength(100);
            RuleFor(x => x.RequestDescription).NotEmpty()
                                                .MaximumLength(200);
        }
    }
}
