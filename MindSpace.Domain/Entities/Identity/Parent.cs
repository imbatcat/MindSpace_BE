using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Domain.Entities.Identity
{
    public class Parent : ApplicationUser
    {
        // 1 Parent - 1 User
        public virtual ApplicationUser User { get; set; }

        // 1 Parent - M TestResponses (for tests taken by parents)
        public virtual ICollection<TestResponse> TestResponses { get; set; } = new HashSet<TestResponse>();
    }
}