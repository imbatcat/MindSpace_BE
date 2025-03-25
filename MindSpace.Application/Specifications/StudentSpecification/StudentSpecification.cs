using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Specifications.StudentSpecification
{
    public class StudentSpecification : BaseSpecification<Student>
    {
        public StudentSpecification(int id) : base(
            s => s.Id.Equals(id) &&
                 s.Status == UserStatus.Enabled)
        {
        }


    }
}
