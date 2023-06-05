using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.Shared
{
    public class CommentListDto
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string Comment { get; set; }
        public int? TopCommentId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
