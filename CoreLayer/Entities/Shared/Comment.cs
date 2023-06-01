using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Shared
{
    public class Comment:Base
    {
        public string Content { get; set; }
        public int TopCommentId { get; set; } = 0;//Eğer 0 dan büyükse bir yorumun cevabıdır.
        public int? SharedId { get; set; }
        public Shared Shared { get; set; }
        public int? UserId { get; set; }
        public User.User User { get; set; }
    }
}
