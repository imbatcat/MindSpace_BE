using Microsoft.AspNetCore.Identity;

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
