using BusinessLayer.Bases;
using Microsoft.AspNet.Identity;

namespace BusinessLayer.AppService
{
    public class RoleAppService : AppServiceBase
    {
        public IdentityResult Create(string rolename)
        {
            return TheUnitOfWork.Role.Create(rolename);//UnitOfWork.Repository.MethodsInRepository
        }
    }
}
