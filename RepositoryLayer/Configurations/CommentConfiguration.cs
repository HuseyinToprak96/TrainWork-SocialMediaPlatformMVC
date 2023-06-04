using CoreLayer.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    internal class CommentConfiguration: EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            Property(x => x.Content).IsRequired().HasMaxLength(250); 
        }
    }
}
