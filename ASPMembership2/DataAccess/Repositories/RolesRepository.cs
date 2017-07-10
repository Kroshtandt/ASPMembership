using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ASPMembership2.DataAccess.Repositories
{
    public class RolesRepository : IGenericDataRepository<AspNetRoles>
    {
        private readonly GenericDataRepository<AspNetRoles> iRoles;

        public RolesRepository()
        {
            iRoles = new GenericDataRepository<AspNetRoles>();
        }

        public void Add(params AspNetRoles[] items)
        {
            iRoles.Add(items);
        }

        public IList<AspNetRoles> GetAll(params Expression<Func<AspNetRoles, object>>[] navigationProperties)
        {
            return iRoles.GetAll(navigationProperties);
        }

        public IList<AspNetRoles> GetList(Func<AspNetRoles, bool> where, params Expression<Func<AspNetRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public AspNetRoles GetSingle(Func<AspNetRoles, bool> where, params Expression<Func<AspNetRoles, object>>[] navigationProperties)
        {
            return iRoles.GetSingle(where, navigationProperties);
        }

        public void Remove(params AspNetRoles[] items)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(string roleId)
        {
            var context = new EntityModel();

            var role = context.AspNetRoles.FirstOrDefault(r => r.Id == roleId);

            role.Methods.Clear();

            context.Entry(role).State = EntityState.Modified;
            context.SaveChanges();

            context.Dispose();
        }

        public void Update(params AspNetRoles[] items)
        {
            iRoles.Update(items);
        }
    }
}