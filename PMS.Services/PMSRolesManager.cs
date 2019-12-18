using PMS.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Entities
{
    public class PMSRoleManager : RoleManager<IdentityRole>
    {
        public PMSRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {
        }
        public static PMSRoleManager Create(IdentityFactoryOptions<PMSRoleManager> options, IOwinContext context)
        {
            return new PMSRoleManager(new RoleStore<IdentityRole>(context.Get<PMSContext>()));
        }
    }
}