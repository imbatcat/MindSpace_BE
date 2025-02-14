using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
