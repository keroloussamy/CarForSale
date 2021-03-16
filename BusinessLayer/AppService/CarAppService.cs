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
    public class CarAppService : AppServiceBase
    {
        #region CURD

        public List<CarVM> GetAllCar()
        {

            return Mapper.Map<List<CarVM>>(TheUnitOfWork.Car.GetAllCars());
        }
        public CarVM GetCar(int id)
        {
            return Mapper.Map<CarVM>(TheUnitOfWork.Car.GetCarById(id));
        }
        //public List<CarVM> AdvSearch(SearchCarVM searchCarVM)
        //{
        //    return Mapper.Map<CarVM>(TheUnitOfWork.Car.GetCarById(id));
        //}


        public bool SaveNewCar(CarVM carVM)
        {
            bool result = false;
            var car = Mapper.Map<Car>(carVM);
            if (TheUnitOfWork.Car.Insert(car))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateCar(CarVM carVM)
        {
            var car = Mapper.Map<Car>(carVM);
            TheUnitOfWork.Car.Update(car);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteCar(int id)
        {
            bool result = false;

            TheUnitOfWork.Car.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckCarExists(CarVM carVM)
        {
            Car car = Mapper.Map<Car>(carVM);
            return TheUnitOfWork.Car.CheckCarExists(car);
        }
        #endregion

    }
}
