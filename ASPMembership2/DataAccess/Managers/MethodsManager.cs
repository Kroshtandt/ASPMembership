using ASPMembership2.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ASPMembership2.DataAccess.Managers
{
    public class MethodsManager
    {
        public static string[] GetRolesByMethodIdentity(string methodIdentity)
        {
            var navProps = new Expression<Func<Methods, object>>[]
            {
                a => a.AspNetRoles
            };

            var repository = new MethodsRepository();
            var response = repository.GetSingle(m => m.MethodId == methodIdentity, navProps);

            return response.AspNetRoles.Select(r => r.Name).ToArray();
        }

        public static List<Methods> GetApplicationMethodsList()
        {
            var repository = new MethodsRepository();
            var response = repository.GetAll();

            return response.Select(m => new Methods
            {
                MethodId = m.MethodId,
                MethodDescription = m.MethodDescription
            }).ToList();
        }
        

        public static void AttachMethodToRole(string roleId, string methodId)
        {
            var repository = new MethodsRepository();
            repository.AttachMethodToRole(roleId, methodId);
        }

        public static void DetachMethodFromRole(string roleId, string methodId)
        {
            var repository = new MethodsRepository();
            repository.DetachMethodFromRole(roleId, methodId);
        }
    }
}