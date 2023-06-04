using CoreLayer.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    internal class SharedLikeConfiguration: EntityTypeConfiguration<SharedLike>
    {
        public SharedLikeConfiguration()
        {
            
        }
    }
}
