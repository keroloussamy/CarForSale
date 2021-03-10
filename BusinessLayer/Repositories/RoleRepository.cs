using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BusinessLayer.Repositories
{
    public class RoleRepository
    {
        ApplicationRoleManager manager;
        public RoleRepository(DbContext db)
        {
            manager = new ApplicationRoleManager(db);
        }

        public IdentityResult Create(string role)
        {
            return manager.CreateAsync(new IdentityRole(role)).Result;
        }

    }
}
