using CoreLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    internal class UserConfiguration: EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(x => x.Username).IsRequired().HasMaxLength(100);
            Property(x=>x.Biography).HasMaxLength(500);
            Property(x=>x.Email).IsRequired().HasMaxLength(120);
            Property(x => x.PhoneNumber).IsRequired().HasMaxLength(15);
            Property(x=>x.Surname).HasMaxLength(30);
            Property(x=>x.Name).HasMaxLength(30);
        }
    }
}
