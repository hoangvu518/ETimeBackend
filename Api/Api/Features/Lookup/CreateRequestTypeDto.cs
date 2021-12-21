using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Features.Lookup
{
    public class CreateRequestTypeDto
    {
        public string RequestTypeName { get; set; }
    }

    public class CreateRequestTypeDtoValidator : AbstractValidator<CreateRequestTypeDto>
    {
        public CreateRequestTypeDtoValidator()
        {
            RuleFor(x => x.RequestTypeName).NotEmpty()
                                    .MaximumLength(50);
        }
    }
}
