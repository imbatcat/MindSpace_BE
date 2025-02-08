using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities;
using System.Linq.Expressions;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterPsychologist.Specifications
{
    public class SpecializationSpecifications : BaseSpecification<Specialization>
    {
        public SpecializationSpecifications(string name) : 
            base(x => 
                x.Name.ToLower() == name.ToLower()
            )
        {
        }
    }
}