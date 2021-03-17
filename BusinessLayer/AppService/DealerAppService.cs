using BusinessLayer.Bases;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AppService
{
    public class DealerAppService : AppServiceBase
    {
        public List<Dealer> GetAllDealer()
        {

            return TheUnitOfWork.Dealer.GetAllDealers();
        }
        public Dealer GetDealer(string id)
        {
            return TheUnitOfWork.Dealer.GetDealerById(id);
        }


        public bool SaveNewDealer(Dealer dealer)
        {
            bool result = false;
            if (TheUnitOfWork.Dealer.Insert(dealer))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateDealer(Dealer dealer)
        {
            TheUnitOfWork.Dealer.Update(dealer);
            TheUnitOfWork.Commit();

            return true;
        }


        //public bool DeleteDealer(int id)
        //{
        //    bool result = false;

        //    TheUnitOfWork.Dealer.Delete(id);
        //    result = TheUnitOfWork.Commit() > new int();

        //    return result;
        //}

        public bool CheckDealerExists(Dealer dealer)
        {
            return TheUnitOfWork.Dealer.CheckDealerExists(dealer);
        }
    }
}
