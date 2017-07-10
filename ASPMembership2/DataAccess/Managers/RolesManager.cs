using ASPMembership2.DataAccess.Repositories;
using ASPMembership2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ASPMembership2.DataAccess.Managers
{
    public class RolesManager
    {
        public static List<AspNetRoles> GetAllRoles()
        {
            var navProps = new Expression<Func<AspNetRoles, object>>[]
            {
                a => a.Methods
            };

            var repo = new RolesRepository();
            var list = repo.GetAll();

            return list.Select(r => new AspNetRoles { Id = r.Id, Name = r.Name }).ToList();
        }

        public static AspNetRoles GetRoleById(string id, bool includeMethods)
        {
            var navProps = new Expression<Func<AspNetRoles, object>>[]
            {
                a => a.Methods
            };

            var repo = new RolesRepository();
            var role = repo.GetSingle(r => r.Id == id,
                includeMethods ?
                navProps :
                new Expression<Func<AspNetRoles, object>>[] { });

            return new AspNetRoles
            {
                Id = role.Id,
                Name = role.Name,
                Methods = role.Methods.Select(m => new Methods { MethodId = m.MethodId, MethodDescription = m.MethodDescription }).ToList()
            };
        }

        public static void SaveRole(AspNetRoles role)
        {
            if (role != null)
            {
                if (role.Id != null)
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.Create()));
                    var newRole = new IdentityRole { Name = role.Name, Id = role.Id };
                    roleManager.Update(newRole);
                }
                else
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.Create()));
                    var newRole = new IdentityRole { Name = role.Name };
                    roleManager.Create(newRole);
                }
            }
        }

        public static void DeleteRoleById(string roleId)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.Create()));
            var role = roleManager.Roles.FirstOrDefault(r => r.Id == roleId);
            roleManager.Delete(role);
        }

        public static AspNetRoles GetRoleByName(string name)
        {
            var repo = new RolesRepository();
            var item = repo.GetSingle(r => r.Name.Equals(name));

            return new AspNetRoles { Id = item.Id, Name = item.Name };
        }
    }
}