using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class CreateRequestTypeDto
    {
        public string RequestTypeName { get; set; } = string.Empty;
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
