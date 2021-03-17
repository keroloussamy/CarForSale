using BusinessLayer.Interfaces;
using BusinessLayer.Repositories;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Common Properties
        private DbContext EC_DbContext { get; set; }

        #endregion

        #region Constructors
        public UnitOfWork()
        {
            EC_DbContext = new ApplicationDBContext();//
            // Avoid load navigation properties
            EC_DbContext.Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region Methods
        public int Commit()
        {
            return EC_DbContext.SaveChanges();
        }

        public void Dispose()
        {
            EC_DbContext.Dispose();
        }
        #endregion


        public CarRepository car;//=> throw new NotImplementedException();
        public CarRepository Car
        {
            get
            {
                if (car == null)
                    car = new CarRepository(EC_DbContext);
                return car;
            }
        }



        public DealerRepository dealer;//=> throw new NotImplementedException();
        public DealerRepository Dealer
        {
            get
            {
                if (dealer == null)
                    dealer = new DealerRepository(EC_DbContext);
                return dealer;
            }
        }


        public AccountRepository account;//=> throw new NotImplementedException();
        public AccountRepository Account
        {
            get
            {
                if (account == null)
                    account = new AccountRepository(EC_DbContext);
                return account;
            }
        }

        public RoleRepository role;//=> throw new NotImplementedException();
        public RoleRepository Role
        {
            get
            {
                if (role == null)
                    role = new RoleRepository(EC_DbContext);
                return role;
            }
        }

        public MessageRepository message;
        public MessageRepository Message
        {
            get
            {
                if (message == null)
                    message = new MessageRepository(EC_DbContext);
                return message;
            }
        }

        public BrandRepository brand;//=> throw new NotImplementedException();
        public BrandRepository Brand
        {
            get
            {
                if (brand == null)
                    brand = new BrandRepository(EC_DbContext);
                return brand;
            }
        }

        //BrandRepository IUnitOfWork.Dealer => throw new NotImplementedException();
    }
}
