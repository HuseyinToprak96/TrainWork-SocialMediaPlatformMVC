using CoreLayer.Dtos.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validations
{
    public class CityValidator:BaseValidator<CityDto>
    {
        public CityValidator()
        {
            RuleFor(x)
        }
    }
}
