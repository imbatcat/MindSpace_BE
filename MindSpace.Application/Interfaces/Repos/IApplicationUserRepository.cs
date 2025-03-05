using MindSpace.Application.Interfaces.Specifications;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Interfaces.Repos
{
    public interface IApplicationUserRepository
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

        /// <summary>
        /// Inserts a new user with the specified password asynchronously.
        /// </summary>
        /// <param name="user">The user to insert.</param>
        /// <param name="password">The password for the new user.</param>
        Task InsertAsync(ApplicationUser user, string password);

        /// <summary>
        /// Inserts multiple users with their corresponding passwords asynchronously.
        /// </summary>
        /// <param name="usersWithPassword">A collection of tuples containing users and their passwords.</param>
        Task InsertBulkAsync(IEnumerable<(ApplicationUser user, string password)> usersWithPassword);

        /// <summary>
        /// Assigns a role to a user asynchronously.
        /// </summary>
        /// <param name="user">The user to assign the role to.</param>
        /// <param name="role">The role to assign.</param>
        Task AssignRoleAsync(ApplicationUser user, string role);

        /// <summary>
        /// Updates an existing user's information asynchronously.
        /// </summary>
        /// <param name="user">The user with updated information.</param>
        Task UpdateAsync(ApplicationUser user);

        /// <summary>
        /// Retrieves a user by their email address asynchronously.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>The user with the specified email, or null if not found.</returns>
        Task<ApplicationUser?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Gets the role of a specified user asynchronously.
        /// </summary>
        /// <param name="user">The user whose role to retrieve.</param>
        /// <returns>The role of the user.</returns>
        Task<string> GetUserRoleAsync(ApplicationUser user);

        /// <summary>
        /// Retrieves a user by their ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        Task<ApplicationUser?> GetByIdAsync(int userId);

        /// <summary>
        /// Toggles the status of a user account asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to toggle.</param>
        Task ToggleAccountStatusAsync(int userId);

        /// <summary>
        /// Filter Users by Role from UserManager
        /// </summary>
        /// <param name="role"></param>
        /// <param name="specParams"></param>
        /// <returns></returns>
        Task<List<ApplicationUser>> FilterUserByRoleAsync(string role, ApplicationUserSpecParams specParams);
    }
}