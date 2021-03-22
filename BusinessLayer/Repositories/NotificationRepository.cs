using BusinessLayer.Bases;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    
    public class NotificationRepository : BaseRepository<Notification>
    {
        private DbContext EC_DbContext;

        public NotificationRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public List<Notification> GetAllNotifications()
        {
            return GetAll().ToList();
        }


        public Notification GetNotificationByUserId(string id)
        {
            return GetWhere(l => l.Id == id).FirstOrDefault();
        }

        public bool InsertNotification(Notification notification)
        {
            return Insert(notification);
        }

        public void UpdateNotification(Notification notification)
        {
            Update(notification);
        }
        public void DeleteNotification(int id)
        {
            Delete(id);
        }

        public bool CheckNotificationExists(Notification notification)
        {
            return GetAny(l => l.Id == notification.Id);
        }

        #endregion
    }
}
