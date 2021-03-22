using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AppService
{
    public class NotificationAppService : BrandAppService
    {
        public List<Notification> GetAllNotification()
        {

            return TheUnitOfWork.Notification.GetAllNotifications();
        }
        public Notification GetNotification(string id)
        {
            return TheUnitOfWork.Notification.GetNotificationByUserId(id);
        }

        public void IncrementNotificationCount(string id)
        {
             var notif = TheUnitOfWork.Notification.GetNotificationByUserId(id);
            notif.MessageNotification++;
            UpdateNotification(notif);

        }

        public void SetNotificationCountToZero(string id)
        {
            var notif = TheUnitOfWork.Notification.GetNotificationByUserId(id);
            notif.MessageNotification =0;
            UpdateNotification(notif);

        }

        public bool SaveNewNotification(Notification notification)
        {
            bool result = false;
            if (TheUnitOfWork.Notification.Insert(notification))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateNotification(Notification notification)
        {
            TheUnitOfWork.Notification.Update(notification);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteNotification(string id)
        {
            bool result = false;

            TheUnitOfWork.Notification.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckNotificationExists(Notification notification)
        {
            return TheUnitOfWork.Notification.CheckNotificationExists(notification);
        }
    }
}
