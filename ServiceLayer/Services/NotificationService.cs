using CoreLayer.Entities.Notification;
using CoreLayer.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class NotificationService:Service<Notification>,INotificationService
    {
    }
}
