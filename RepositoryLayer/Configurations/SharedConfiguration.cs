using CoreLayer.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    internal class SharedConfiguration: EntityTypeConfiguration<Shared>
    {
        public SharedConfiguration()
        {
            Property(x => x.Description).IsRequired().HasMaxLength(500);
            Property(x=>x.Path).HasMaxLength(100);
            Property(x=>x.Title).HasMaxLength(100);
        }
    }
}
