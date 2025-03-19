using MindSpace.Domain.Entities;

namespace MindSpace.Application.Specifications.SchoolSpecifications
{
    public class SchoolSpecifications : BaseSpecification<School>
    {
        public SchoolSpecifications() : base(x => x.Id != null)
        {

        }

        public SchoolSpecifications(int schoolManagerId) : base(x => x.SchoolManagerId == schoolManagerId)
        {
            AddInclude(x => x.SchoolManager);
        }

        public SchoolSpecifications(string schoolName) :
            base(x =>
                x.SchoolName.ToLower() == schoolName.ToLower()
            )
        {
        }
    }
}