using CoreLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    internal class UserProfileImagesConfiguration: EntityTypeConfiguration<UserProfileImages>
    {
        public UserProfileImagesConfiguration()
        {
            Property(x => x.Description).HasMaxLength(200);
            Property(x=>x.Path).HasMaxLength(100);
        }
    }
}
