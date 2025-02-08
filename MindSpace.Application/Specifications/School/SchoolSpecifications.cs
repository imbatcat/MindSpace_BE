using MindSpace.Domain.Entities;
using System.Linq.Expressions;

namespace MindSpace.Application.Specifications.School
{
    public class SchoolSpecifications : BaseSpecification<School>
    {
        public SchoolSpecifications(string schoolName) :
            base(x =>
                x.SchoolName.ToLower() == schoolName.ToLower()
            )
        {
        }
    }
}