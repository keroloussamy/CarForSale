using BusinessLayer.Bases;
using BusinessLayer.ViewModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AppService
{
    public class MessageAppService : AppServiceBase
    {
        #region CURD

        public List<MessageVM> GetAllMessage()
        {

            return Mapper.Map<List<MessageVM>>(TheUnitOfWork.Message.GetAllMessage());
        }
        public MessageVM GetMessage(int id)
        {
            return Mapper.Map<MessageVM>(TheUnitOfWork.Message.GetMessageById(id));
        }



        public bool SaveNewMessage(MessageVM messageViewModel)
        {
            bool result = false;
            var message = Mapper.Map<Message>(messageViewModel);
            if (TheUnitOfWork.Message.Insert(message))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateMessage(MessageVM messageViewModel)
        {
            var message = Mapper.Map<Message>(messageViewModel);
            TheUnitOfWork.Message.Update(message);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteMessage(int id)
        {
            bool result = false;

            TheUnitOfWork.Message.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckMessageExists(MessageVM messageViewModel)
        {
            Message message = Mapper.Map<Message>(messageViewModel);
            return TheUnitOfWork.Message.CheckMessageExists(message);
        }
        #endregion

    }
}
