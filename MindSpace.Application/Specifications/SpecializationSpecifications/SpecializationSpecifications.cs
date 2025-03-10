using MindSpace.Domain.Entities;

namespace MindSpace.Application.Specifications.SpecializationSpecifications
{
    public class SpecializationSpecifications : BaseSpecification<Specialization>
    {
        public SpecializationSpecifications(int id) :
            base(x => x.Id.Equals(id))
        {
        }

        public SpecializationSpecifications(SpecializationSpecParams specParams) : base(
            x => string.IsNullOrEmpty(specParams.Name) || x.Name.ToLower().Equals(specParams.Name!.ToLower()))
        {
        }
    }
}