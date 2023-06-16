using CoreLayer.Entities.Notification;
using CoreLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class NotificationRepository:GenericRepository<Notification>,INotificationRepository
    {
    }
}
