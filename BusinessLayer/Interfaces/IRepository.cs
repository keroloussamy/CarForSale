using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetWhere(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        T GetById(int entityId);

        bool Insert(T entity);
        void InsertList(List<T> entity);

        void Update(T entity);
        void UpdateList(List<T> entity);

        void Delete(T entity);
        void Delete(int entityId);
    }
}
