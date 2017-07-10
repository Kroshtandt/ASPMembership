using ASPMembership2.DataAccess;
using ASPMembership2.DataAccess.Managers;
using ASPMembership2.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ASPMembership2.API
{
    public class RolesAPIController : ApiController
    {
        private const string className = "RolesAPIController";

        [HttpGet]
        [Route("rolesApi/getAllRoles")]
        [AuthorizeRoles(className)]
        public List<AspNetRoles> GetAllRoles()
        {
            var roles = RolesManager.GetAllRoles();
            return roles;
        }


        [HttpGet]
        [Route("rolesApi/getRolesByUserId/{userId}")]
        [AuthorizeRoles(className)]
        public async Task<List<AspNetRoles>> GetRolesByUserId(string userId)
        {
            List<AspNetRoles> rolesList = new List<AspNetRoles>();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
            var roles = await userManager.GetRolesAsync(userId);

            foreach (var stringRole in roles)
            {
                rolesList.Add(RolesManager.GetRoleByName(stringRole));
            }
            return rolesList;
        }


        [HttpGet]
        [Route("rolesApi/getRoleById/{id}/{includeMethods}")]
        [AuthorizeRoles(className)]
        public AspNetRoles GetRoleById(string id, int? includeMethods)
        {
            if (id != null)
            {
                var role = RolesManager.GetRoleById(id,
                    (includeMethods != null && includeMethods > 0));

                return role;
            }
            return null;
        }

        [HttpPost]
        [Route("rolesApi/saveRole")]
        [AuthorizeRoles(className)]
        public void SaveRole([FromBody]AspNetRoles role)
        {
            if (role != null)
            {
                RolesManager.SaveRole(role);
            }
        }


        [HttpPost]
        [Route("rolesApi/attachRoleToUser/{userId}/{roleName}")]
        [AuthorizeRoles(className)]
        public void AttachRoleToUser(string userId, string roleName)
        {
            if (userId != null && roleName != null)
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
                userManager.AddToRoleAsync(userId, roleName);
            }

        }

        [HttpPost]
        [Route("rolesApi/detachRoleFromUser/{userId}/{roleName}")]
        [AuthorizeRoles(className)]
        public void DetachRoleFromUser(string userId, string roleName)
        {
            if (userId != null && roleName != null)
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
                userManager.RemoveFromRoleAsync(userId, roleName);
            }
        }
    }
}
