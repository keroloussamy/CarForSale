using BusinessLayer.Bases;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AppService
{
    public class EmployeeAppService : AppServiceBase
    {
        public List<Employee> GetAllEmployee()
        {

            return TheUnitOfWork.Employee.GetAllEmployees();
        }
        public List<Employee> GetAllEmployeeWhere(Expression<Func<Employee, bool>> filter)
        {

            return TheUnitOfWork.Employee.GetEmployeesWhere(filter);
        }
        public ApplicationUserIdentity GetEmployeeAsUSer(string id)
        {
            return TheUnitOfWork.Account.Find(id);
        }
        public Employee GetEmployee(string id)
        {
            return TheUnitOfWork.Employee.GetEmployeeById(id);
        }

        public bool SaveNewEmployee(Employee employee)
        {
            bool result = false;
            if (TheUnitOfWork.Employee.Insert(employee))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateEmployee(ApplicationUserIdentity employee)
        {
            TheUnitOfWork.Account.Update(employee);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteEmployee(string id)
        {
            bool result = false;

            TheUnitOfWork.Employee.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckEmployeeExists(Employee employee)
        {
            return TheUnitOfWork.Employee.CheckEmployeeExists(employee);
        }
    }
}
