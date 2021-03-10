using BusinessLayer.Bases;
using BusinessLayer.ViewModels;
using DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AppService
{
    public class AccountAppService : AppServiceBase
    {
        //Login
        public ApplicationUserIdentity Find(string name, string passworg)
        {
            return TheUnitOfWork.Account.Find(name, passworg);
        }
        public IdentityResult Register(RegisterVM user)
        {
            ApplicationUserIdentity identityUser =
                Mapper.Map<RegisterVM, ApplicationUserIdentity>(user);
            return TheUnitOfWork.Account.Register(identityUser);

        }
        public IdentityResult AssignToRole(string userid, string rolename)
        {
            return TheUnitOfWork.Account.AssignToRole(userid, rolename);
        }
    }
}
