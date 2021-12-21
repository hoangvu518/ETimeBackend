using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Features.Employee
{
    public class CreateEmployeeDto
    {
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public int? ManagerId { get;  set; }
        public decimal? Salary { get;  set; }
        public string Email { get;  set; }
    }

    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator()
        {

            RuleFor(x => x.FirstName).NotEmpty()
                                     .MaximumLength(50);

            RuleFor(x => x.LastName).NotEmpty()
                                    .MaximumLength(50);
            RuleFor(x => x.Salary).InclusiveBetween(1, 999999);
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
