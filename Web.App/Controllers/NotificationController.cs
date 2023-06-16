using CoreLayer.Dtos.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace Web.App.Controllers
{
    public class NotificationController : BaseController
    {
        // GET: Notification
        public ActionResult GetNavNotifications()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            var result=_notificationService.Where(x=>x.UserId == UserId);
            List<NotificationDto> notificationList = new List<NotificationDto>();
            result.Data.OrderByDescending(x=>x.Id).ForEach(x =>
            {
                NotificationDto notification = new NotificationDto { Id = x.Id, Content = x.Content, IsSeen = x.IsSeen, Link = x.Link };
                notificationList.Add(notification);
            } );
            return PartialView(notificationList);
        }
    }
}