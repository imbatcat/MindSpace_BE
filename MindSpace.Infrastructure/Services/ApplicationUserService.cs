using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Specifications;
using MindSpace.Application.Specifications;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Infrastructure.Services
{
    public class ApplicationUserService<T> : IApplicationUserService<T> where T : ApplicationUser
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

        public async Task<IReadOnlyList<T>> GetAllUsersAsync()
        {
            return await _userManager.Users.OfType<T>().ToListAsync();
        }

        public async Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }

        public async Task<IReadOnlyList<T>> GetAllUsersWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<T?> GetUserWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            var query = _userManager.Users.OfType<T>().AsQueryable();
            return await SpecificationQueryBuilder<T>.BuildCountQuery(query, spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var query = _userManager.Users.OfType<T>().AsQueryable();
            return SpecificationQueryBuilder<T>.BuildQuery(query, spec);
        }

        public Task InsertBulkAsync(IEnumerable<(ApplicationUser user, string password)> usersWithPassword)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task InsertAsync(ApplicationUser user, string password)
        {
            // Check for duplicate email
            var existingUserByEmail = await _userManager.FindByEmailAsync(user.Email!);
            if (existingUserByEmail != null)
            {
                throw new DuplicateUserException($"A user with the email {user.Email} already exists.");
            }

            // Check for duplicate username
            var existingUserByUsername = await _userManager.FindByNameAsync(user.UserName!);
            if (existingUserByUsername != null)
            {
                throw new DuplicateUserException($"A user with the username {user.UserName} already exists.");
            }

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new CreateFailedException(user.Email!);
            }
        }

        public async Task AssignRoleAsync(T user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GetUserRoleAsync(T user)
        {
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
        }

        public async Task<ApplicationUser?> GetByIdAsync(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task ToggleAccountStatusAsync(int userId)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(T), userId.ToString());
            }

            user.Status = user.Status == UserStatus.Enabled ? UserStatus.Disabled : UserStatus.Enabled;
            await UpdateAsync(user);
        }

        public async Task<List<ApplicationUser>> FilterUserByRoleAsync(string role, ApplicationUserSpecParams specParams)
        {
            IList<ApplicationUser> usersInRole = await _userManager.GetUsersInRoleAsync(role);

            var specification = new ApplicationUserSpecification(specParams, isOnlyStudent: false);

            var filteredUsers = usersInRole.AsQueryable().Where(specification.Criteria);

            if (specification.OrderBy != null)
            {
                filteredUsers = filteredUsers.OrderBy(specification.OrderBy);
            }

            int skip = specification.Skip;
            int take = specification.Take;
            var pagedUsers = filteredUsers.Skip(skip).Take(take).ToList();

            return pagedUsers;
        }

        public async Task<int> CountUserByRoleAsync(string role, DateTime? startDate, DateTime? endDate)
        {
            IList<ApplicationUser> usersInRole = await _userManager.GetUsersInRoleAsync(role);

            var specification = new ApplicationUserSpecification(startDate, endDate);

            var query = usersInRole.AsQueryable().Where(specification.Criteria);
            return query.Count();
        }
    }
}