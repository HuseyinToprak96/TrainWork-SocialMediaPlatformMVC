using CoreLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Notification
{
    public class Notification
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User.User User { get; set; }
        public string Content { get; set; }
        public bool IsSeen { get; set; } = false;//Görüldü mü? Görülmemiş olan bildirimler kullanıcıya 1 2 3 şeklinde gösterilecek. göründükten sonra true olacak
        public string Link { get; set; }//Bilgirime tıkladığımızda açılacak Controller/Action
        public ENotificationType NotificationType { get; set; }
    }
}
