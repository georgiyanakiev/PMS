using Microsoft.AspNet.Identity.EntityFramework;
using PMS.Entities;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.Dashboard.ViewModels
{
    public class UsersListingModel
    {
        public IEnumerable<PMSUser> Users { get; set; }

        public string RoleID { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class UserActionModel
    {
        public int ID { get; set; }

        public string RoleID { get; set; }
        public IdentityRole Role { get; set; }

        public string Name { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}