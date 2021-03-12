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
   public class BrandRepository: BaseRepository<Brand>
    {

        private DbContext EC_DbContext;

        public BrandRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public List<Brand> GetAllBrands()
        {
            return GetAll().ToList();
        }

        public Brand GetBrandById(int id)
        {
            return GetFirstOrDefault(l => l.Id == id);
        }

        public bool InsertBrand(Brand brand)
        {
            return Insert(brand);
        }

        public void UpdateBrand(Brand brand)
        {
            Update(brand);
        }

        public void DeleteBrand(int id)
        {
            Delete(id);
        }

      

        #endregion
    }
}
