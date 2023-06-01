using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.Notification
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public bool IsSeen { get; set; }
    }
}
