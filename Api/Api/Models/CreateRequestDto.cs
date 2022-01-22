using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class CreateRequestDto
    {
        public string RequestTitle { get; set; } = string.Empty;
        public string RequestDescription { get; set; } = string.Empty;
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
