using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities;
using System.Linq.Expressions;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterStudent.Specifications
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