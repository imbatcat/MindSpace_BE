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

        public SchoolSpecifications(int schoolId, bool isGetBySchoolId = true) :
            base(x =>
                x.Id == schoolId
            )
        {
        }

        public SchoolSpecifications(string schoolName) :
            base(x =>
                x.SchoolName.ToLower() == schoolName.ToLower()
            )
        {
        }

        public SchoolSpecifications(DateTime? startDate, DateTime? endDate) : base(s 
            => (!startDate.HasValue || s.CreateAt >= startDate)
               && (!endDate.HasValue || s.CreateAt <= endDate))
        {
            AddInclude(x => x.SchoolManager);
        }
    }
}