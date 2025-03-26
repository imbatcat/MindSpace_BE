using MindSpace.Domain.Entities;

namespace MindSpace.Application.Specifications.SpecializationSpecifications
{
    public class SpecializationSpecification : BaseSpecification<Specialization>
    {
        public SpecializationSpecification(int id) :
            base(x => x.Id.Equals(id))
        {
        }

        public SpecializationSpecification(SpecializationSpecParams specParams) : base(
            x => string.IsNullOrEmpty(specParams.Name) || x.Name.ToLower().Equals(specParams.Name!.ToLower()))
        {
        }
    }
}