using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class UpdateManagerDto
    {
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }
    }

    public class UpdateManagerDtoValidator : AbstractValidator<UpdateManagerDto>
    {
        public UpdateManagerDtoValidator()
        {
            RuleFor(x => x.EmployeeId).NotNull().WithMessage($"EmployeeId is required");
            RuleFor(x => x.ManagerId).NotNull().WithMessage($"ManagerId is required");
        }
    }
}
