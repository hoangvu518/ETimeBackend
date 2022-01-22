using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? ManagerId { get;  set; }
        public decimal? Salary { get;  set; }
        public string Email { get; set; } = string.Empty;
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
