using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ASPMembership2.DataAccess.Repositories
{
    public class MethodsRepository : IGenericDataRepository<Methods>
    {
        private readonly GenericDataRepository<Methods> iMethods;

        public MethodsRepository()
        {
            iMethods = new GenericDataRepository<Methods>();
        }

        public void Add(params Methods[] items)
        {
            iMethods.Add(items);
        }

        public IList<Methods> GetAll(params Expression<Func<Methods, object>>[] navigationProperties)
        {
            return iMethods.GetAll(navigationProperties);
        }

        public IList<Methods> GetList(Func<Methods, bool> where, params Expression<Func<Methods, object>>[] navigationProperties)
        {
            return iMethods.GetList(where, navigationProperties);
        }

        public Methods GetSingle(Func<Methods, bool> where, params Expression<Func<Methods, object>>[] navigationProperties)
        {
            return iMethods.GetSingle(where, navigationProperties);
        }

        public void Remove(params Methods[] items)
        {
            iMethods.Remove(items);
        }

        public void AttachMethodToRole(string roleId, string methodId)
        {
            var context = new EntityModel();

            var role = context.AspNetRoles.FirstOrDefault(r => r.Id == roleId);
            var method = context.Methods.FirstOrDefault(m => m.MethodId == methodId);

            role.Methods.Add(method);

            context.Entry(role).State = EntityState.Modified;
            context.SaveChanges();

            context.Dispose();
        }

        public void DetachMethodFromRole(string roleId, string methodId)
        {
            var context = new EntityModel();

            var role = context.AspNetRoles.FirstOrDefault(r => r.Id == roleId);
            var method = context.Methods.FirstOrDefault(m => m.MethodId == methodId);

            role.Methods.Remove(method);

            context.Entry(role).State = EntityState.Modified;
            context.SaveChanges();

            context.Dispose();
        }

        public void Update(params Methods[] items)
        {
            iMethods.Update(items);
        }
    }
}