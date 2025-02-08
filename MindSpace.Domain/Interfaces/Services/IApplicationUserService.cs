using Microsoft.AspNetCore.Identity;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface IApplicationUserService<T> where T : IdentityUser<int>
    {
        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <returns>A read-only list of all users.</returns>
        Task<IReadOnlyList<T>> GetAllUsersAsync();
        /// <summary>
        /// Retrieves users by their role asynchronously.
        /// </summary>
        /// <param name="role">The role of the users to retrieve.</param>
        /// <returns>A list of users with the specified role.</returns>
        Task<IList<T>> GetUsersByRoleAsync(string role);
        /// <summary>
        /// Retrieves all users that match a given specification asynchronously.
        /// </summary>
        /// <param name="spec">The specification to filter users.</param>
        /// <returns>A read-only list of users that match the specification.</returns>
        Task<IReadOnlyList<T>> GetAllUsersWithSpecAsync(ISpecification<T> spec);
        /// <summary>
        /// Retrieves a single user that matches a given specification asynchronously.
        /// </summary>
        /// <param name="spec">The specification to filter the user.</param>
        /// <returns>The user that matches the specification, or null if no user matches.</returns>
        Task<T?> GetUserWithSpec(ISpecification<T> spec);

        /// <summary>
        /// Counts the number of users that match a given specification asynchronously.
        /// </summary>
        /// <param name="spec">The specification to filter users.</param>
        /// <returns>The count of users that match the specification.</returns>
        Task<int> CountAsync(ISpecification<T> spec);

        Task<int> InsertAsync(T user, string password);
    }
}
