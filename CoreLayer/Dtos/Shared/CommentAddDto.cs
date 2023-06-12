using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.Shared
{
    public class CommentAddDto
    {
        public int UserId { get; set; }
        public int SharedId { get; set; }
        public int TopCommentId { get; set; }
        public string Comment { get; set; }

    }
}
