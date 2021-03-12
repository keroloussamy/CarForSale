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
    public class BrandAppService: AppServiceBase
    {
        public List<BrandVM> GetBrands()
        {
            return Mapper.Map<List<BrandVM>>(TheUnitOfWork.Brand.GetAll());
        }


        public BrandVM GetBrand(int id)
        {
            return Mapper.Map<BrandVM>(TheUnitOfWork.Brand.GetById(id));
        }



        public bool insertNewBrand(BrandVM brandVM)
        {
            bool result = false;
            var brand = Mapper.Map<Brand>(brandVM);
            if (TheUnitOfWork.Brand.Insert(brand))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateBrand(BrandVM brandVM)
        {
            var brand = Mapper.Map<Brand>(brandVM);
            TheUnitOfWork.Brand.Update(brand);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteBrand(int id)
        {
            bool result = false;

            TheUnitOfWork.Brand.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

      
    }
}
