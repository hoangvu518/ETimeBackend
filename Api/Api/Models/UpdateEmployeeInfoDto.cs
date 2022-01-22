using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class UpdateEmployeeInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UpdateEmployeeInfoDtoValidator : AbstractValidator<UpdateEmployeeInfoDto>
    {
        public UpdateEmployeeInfoDtoValidator()
        {

            RuleFor(x => x.LastName).NotEmpty()
                                    .MaximumLength(10);
            RuleFor(x => x.FirstName).NotEmpty()
                                     .MaximumLength(10);
            RuleFor(x => x.Email).NotEmpty()
                                    .EmailAddress();
        }
    }
}
