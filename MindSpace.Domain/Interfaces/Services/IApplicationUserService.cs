using Microsoft.AspNetCore.Identity;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface IApplicationUserService<T> where T : IdentityUser<int>
    {
        // GET
        Task<IReadOnlyList<T>> GetAllUsersAsync();
        Task<IList<T>> GetUsersByRoleAsync(string role);
        Task<IReadOnlyList<T>> GetAllUsersWithSpecAsync(ISpecification<T> spec);
        Task<T?> GetUserWithSpec(ISpecification<T> spec);

        // COUNT
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
