using CoreLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.User
{
    public class UserSearchFilterDto
    {
        public string Search { get; set; }//Name Surname Username veya email veya phone number
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public EGender Gender { get; set; }
    }
}
