using BusinessLayer.Bases;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        private DbContext EC_DbContext;

        public EmployeeRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public List<Employee> GetAllEmployees()
        {
            return GetAll().Include(d => d.User).Include(e=>e.User.Address).Include(e=>e.User.Roles).ToList();
        }

        public List<Employee> GetEmployeesWhere(Expression<Func<Employee, bool>> filter)
        {
            return GetWhere(filter).Include(d => d.User).Include(e => e.User.Address).Include(e => e.User.Roles).ToList();
        }

        public Employee GetEmployeeById(string id)
        {
            return GetWhere(l => l.Id == id).Include(d => d.User).Include(c => c.User.Address).FirstOrDefault();
        }

        public bool InsertEmployee(Employee employee)
        {
            return Insert(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }

        public void DeleteEmployee(string id)     //id should be string here for user identity
        {
            Delete(id);
        }

        public bool CheckEmployeeExists(Employee employee)
        {
            return GetAny(l => l.Id == employee.Id);
        }

        #endregion
    }
}
