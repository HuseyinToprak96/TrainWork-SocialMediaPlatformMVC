using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.HelperTables
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<District> Districts { get; set; }
    }
}
