using CoreLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.Shared
{
    public class SharedAddorUpdateDto
    {
        public int SharedId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int? UserId { get; set; }
        public int? CreatedUserId { get; set; }
        public bool IsActive { get; set; }
        public EFileType Type { get; set; }
        
    }
}
