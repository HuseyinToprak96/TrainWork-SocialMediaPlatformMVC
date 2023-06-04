using CoreLayer.Dtos.User;
using CoreLayer.Entities.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validations
{
    internal class UserValidator: BaseValidator<CreateUserDto>
    {
        public UserValidator()
        {
            RuleFor(x=>x.Email).NotNull().WithMessage(NotNullorEmptyMessage).NotEmpty().WithMessage(NotNullorEmptyMessage).MaximumLength(120).WithMessage(MaxLengthMessage);
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage(NotNullorEmptyMessage).NotEmpty().WithMessage(NotNullorEmptyMessage).MinimumLength(15).WithMessage(MinLengthMessage);
            RuleFor(x => x.Password).NotNull().WithMessage(NotNullorEmptyMessage).NotEmpty().WithMessage(NotNullorEmptyMessage).Length(8, 15); 
        }
    }
}
