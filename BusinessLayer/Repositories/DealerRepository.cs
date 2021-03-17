﻿using BusinessLayer.Bases;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class DealerRepository : BaseRepository<Dealer>
    {
        private DbContext EC_DbContext;

        public DealerRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public List<Dealer> GetAllDealers()
        {
            return GetAll().Include(d => d.User).ToList();
        }



        public Dealer GetDealerById(string id)
        {
            return GetFirstOrDefault(l => l.Id == id);
        }

        public bool InsertDealer(Dealer dealer)
        {
            return Insert(dealer);
        }

        public void UpdateDealer(Dealer dealer)
        {
            Update(dealer);
        }

        //public void DeleteDealer(int id)     //id should be string here
        //{
        //    Delete(id);
        //}

        public bool CheckDealerExists(Dealer dealer)
        {
            return GetAny(l => l.Id == dealer.Id);
        }

        #endregion
    }
}
