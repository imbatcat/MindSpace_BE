using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Specifications.ApplicationUserSpecifications
{
    public class IdentityUserRoleSpecification : BaseSpecification<IdentityUserRole<int>>
    {
        public IdentityUserRoleSpecification(
            int roleId) : base(x => x.RoleId == roleId)
        {
        }
    }
}
