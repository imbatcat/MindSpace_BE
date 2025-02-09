using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.Domain.Interfaces.Services.Authentication
{
    public interface IApplicationUserService
    {
        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <returns>A read-only list of all users.</returns>
        Task<IReadOnlyList<ApplicationUser>> GetAllUsersAsync();
        /// <summary>
        /// Retrieves users by their role asynchronously.
        /// </summary>
        /// <param name="role">The role of the users to retrieve.</param>
        /// <returns>A list of users with the specified role.</returns>
        Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role);
        /// <summary>
        /// Retrieves all users that match a given specification asynchronously.
        /// </summary>
        /// <param name="spec">The specification to filter users.</param>
        /// <returns>A read-only list of users that match the specification.</returns>
        Task<IReadOnlyList<ApplicationUser>> GetAllUsersWithSpecAsync(ISpecification<ApplicationUser> spec);
        /// <summary>
        /// Retrieves a single user that matches a given specification asynchronously.
        /// </summary>
        /// <param name="spec">The specification to filter the user.</param>
        /// <returns>The user that matches the specification, or null if no user matches.</returns>
        Task<ApplicationUser?> GetUserWithSpec(ISpecification<ApplicationUser> spec);

        /// <summary>
        /// Counts the number of users that match a given specification asynchronously.
        /// </summary>
        /// <param name="spec">The specification to filter users.</param>
        /// <returns>The count of users that match the specification.</returns>
        Task<int> CountAsync(ISpecification<ApplicationUser> spec);

        Task InsertAsync(ApplicationUser user, string password);

        Task InsertBulkAsync(IEnumerable<(ApplicationUser user, string password)> usersWithPassword);

        Task UpdateAsync(ApplicationUser user);
    }
}
