using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.User
{
    public class UserPhotoDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsMain { get; set; }
    }
}
