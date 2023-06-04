using CoreLayer.Entities.Shared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validations
{
    public class SharedValidator:BaseValidator<Shared>
    {
        public SharedValidator()
        {
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x=>x.Title).MaximumLength(100);
            RuleFor(x=>x.Path).MaximumLength(100);
        }
    }
}
