using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;

namespace MindSpace.Application.Specifications.ResourceSpecifications
{
    public class ResourceSpecification : BaseSpecification<Resource>
    {
        /// <summary>
        /// Filter by searching title
        /// </summary>
        /// <param name="title"></param>
        public ResourceSpecification(string title)
            : base(x => x.Title.ToLower().Contains(title.ToLower()))
        {
            AddOrderBy(x => x.Id);
        }

        /// <summary>
        /// Filter by Id and Type
        /// </summary>
        /// <param name="id"></param>
        public ResourceSpecification(int id, ResourceType type)
            : base(x => x.ResourceType.Equals(type) && x.Id == id)
        {
            AddInclude(x => x.SchoolManager);
            AddInclude(x => x.Specialization);
        }

        /// <summary>
        /// General Filter
        /// </summary>
        /// <param name="specParams"></param>
        public ResourceSpecification(ResourceSpecificationSpecParams specParams)
            : base(x =>
                (!specParams.SchoolManagerId.HasValue || x.SchoolManagerId == specParams.SchoolManagerId) &&
                (!specParams.SpecializationId.HasValue || x.SpecializationId == specParams.SpecializationId) &&
                (!specParams.IsActive.HasValue || x.isActive == specParams.IsActive) &&
                (string.IsNullOrEmpty(specParams.SearchTitle) || x.Title.Contains(specParams.SearchTitle)) &&
                (string.IsNullOrEmpty(specParams.Type) || x.ResourceType == (ResourceType)Enum.Parse(typeof(ResourceType), specParams.Type)))
        {
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            AddOrderByDescending(x => x.Id);
        }

        public ResourceSpecification(int schoolId, DateTime? startDate, DateTime? endDate) : base(
            a => a.SchoolManager.SchoolId == schoolId
            && (!startDate.HasValue || a.CreateAt >= startDate)
            && (!endDate.HasValue || a.CreateAt <= endDate)
            )
        {

            AddInclude(a => a.SchoolManager);
        }
    }
}
