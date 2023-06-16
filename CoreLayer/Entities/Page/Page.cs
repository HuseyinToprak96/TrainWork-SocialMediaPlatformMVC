using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Page
{
    public class Page:Base
    {
        public string LinkName { get; set; }
        public string Route { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}
