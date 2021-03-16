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
    public class CarRepository : BaseRepository<Car>
    {
        private DbContext EC_DbContext;

        public CarRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public List<Car> GetAllCars()
        {
            return GetAll().Include(x => x.Dealer.User).Include(x => x.Dealer).Include(x =>x.Dealer.User.Address).ToList();//
        }

        //public List<Car> AdvSearch(Car car)
        //{
        //    var result = GetAll();
        //    if (car != null)
        //    {
        //        if (car.Condition != null)
        //            result = result.Where(x => x.Id == car.Id);
        //        if (!string.IsNullOrEmpty(car.Name))
        //            result = result.Where(x => x.Name.Contains(car.Name));
        //        if (car.PriceFrom.HasValue)
        //            result = result.Where(x => x.Price >= car.PriceFrom);
        //        if (car.PriceTo.HasValue)
        //            result = result.Where(x => x.Price <= car.PriceTo);
        //    }
        //    return result;
        //}

        public Car GetCarById(int id)
        {
            return GetFirstOrDefault(l => l.Id == id);
        }

        public bool InsertCar(Car car)
        {
            return Insert(car);
        }

        public void UpdateCar(Car car)
        {
            Update(car);
        }
        public void DeleteCar(int id)
        {
            Delete(id);
        }

        public bool CheckCarExists(Car car)
        {
            return GetAny(l => l.Id == car.Id);
        }
        
        #endregion
    }
}
