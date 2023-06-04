using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.Shared
{
    public class CommentAnswer
    {
        public int TopCommentId { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
    }
}
