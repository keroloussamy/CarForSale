using DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class AccountRepository
    {
        ApplicationUserManager manager;

        public AccountRepository(DbContext db)
        {
            manager = new ApplicationUserManager(db);
        }
        public ApplicationUserIdentity Find(string username, string password)
        {
            ApplicationUserIdentity result = manager.Find(username, password);
            return result;

        }
        public IdentityResult Register(ApplicationUserIdentity user)
        {
            IdentityResult result = manager.Create(user, user.PasswordHash);
            return result;
        }
        public IdentityResult AssignToRole(string userid, string rolename)
        {
            IdentityResult result = manager.AddToRole(userid, rolename);
            return result;

        }
    }
}
