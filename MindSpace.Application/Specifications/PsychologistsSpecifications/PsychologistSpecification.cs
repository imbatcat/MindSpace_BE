using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Specifications.PsychologistsSpecifications
{
    public class PsychologistSpecification : BaseSpecification<Psychologist>
    {
        public PsychologistSpecification(int psychologistId) : base(x => x.Id == psychologistId)
        {
            AddInclude(x => x.Feedbacks);
            AddInclude("Feedbacks.Students");
        }

        public PsychologistSpecification(PsychologistSpecParams specParams, bool isIncludeFeedbacks) : base(
            x =>
            (string.IsNullOrEmpty(specParams.SearchName) || x.FullName.ToLower().Contains(specParams.SearchName.ToLower())))
        {
            if (isIncludeFeedbacks)
            {
                AddInclude(x => x.Feedbacks);
                AddInclude("Feedbacks.Students");
            }

            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    }
}
