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
    public class MessageRepository : BaseRepository<Message>
    {
        private DbContext EC_DbContext;

        public MessageRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public List<Message> GetAllMessage()
        {
            return GetAll().ToList();
        }

        public bool InsertMessage(Message Message)
        {
            return Insert(Message);
        }
        public void UpdateMessage(Message Message)
        {
            Update(Message);
        }
        public void DeleteMessage(int id)
        {
            Delete(id);
        }

        public bool CheckMessageExists(Message Message)
        {
            return GetAny(l => l.Id == Message.Id);
        }
        public Message GetMessageById(int id)
        {
            return GetFirstOrDefault(l => l.Id == id);
        }
        #endregion
    }
}
