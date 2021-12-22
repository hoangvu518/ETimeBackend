using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Features.Employee
{
    public class UpdateEmployeeInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName {get;set;}
        public string Email { get; set; }
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
