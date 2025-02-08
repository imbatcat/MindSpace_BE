using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Domain.Interfaces.Specifications;


namespace MindSpace.Application.Services
{
    public class ApplicationUserService : IApplicationUserService<ApplicationUser>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        // ================================
        // === Constructors
        // ================================

        public ApplicationUserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<IReadOnlyList<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllUsersWithSpecAsync(ISpecification<ApplicationUser> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserWithSpec(ISpecification<ApplicationUser> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<ApplicationUser> spec)
        {
            var query = _userManager.Users.AsQueryable();
            return await SpecificationQueryBuilder<ApplicationUser>.BuildCountQuery(query, spec).CountAsync();
        }

        private IQueryable<ApplicationUser> ApplySpecification(ISpecification<ApplicationUser> spec)
        {
            var query = _userManager.Users.AsQueryable();
            return SpecificationQueryBuilder<ApplicationUser>.BuildQuery(query, spec);
        }

        public async Task<int> InsertAsync(ApplicationUser user, string password)
        {
            var createdUser = await _userManager.CreateAsync(user, password);
        }
    }
}
