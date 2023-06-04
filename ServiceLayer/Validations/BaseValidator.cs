using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validations
{
    public class BaseValidator<T>: AbstractValidator<T>
    {
        public string NotNullorEmptyMessage { get; } = "{ProperyName} boş geçilemez!";
        public string MaxLengthMessage { get; } = "{PropertyName} maximum {MaxLength} karakter olabilir.";
        public string MinLengthMessage { get; } = "{PropertyName} minimum {MinLength} karakter olabilir.";
    }
}
