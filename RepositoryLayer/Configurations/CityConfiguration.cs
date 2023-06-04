using CoreLayer.Entities.HelperTables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    internal class CityConfiguration: EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(30);
        }
    }
}
