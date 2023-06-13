using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.Shared
{
    public class SharedFilterDto
    {
        public int sharedType { get; set; } = 0;
        public int WhoUsers { get; set; } = 1;
        public string username { get; set; } = "";
    }
}
