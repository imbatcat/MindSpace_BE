using MindSpace.Domain.Entities;

namespace MindSpace.Application.Specifications.SpecializationSpecifications
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