using CoreLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.Shared
{
    public class SharedListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public int LikeCount { get; set; }
        public bool isLike { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        public EFileType Type { get; set; }
        public List<CommentListDto> CommentList { get; set; }
    }
}
