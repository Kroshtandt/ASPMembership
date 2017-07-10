using ASPMembership2.DataAccess;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMembership2.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public IdentityUserRole UserRole { get; set; }
    }
}