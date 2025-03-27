using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Specifications.PsychologistsSpecifications
{
    public class PsychologistSpecification : BaseSpecification<Psychologist>
    {
        public PsychologistSpecification() : base(x => true) 
        {
            AddInclude(x => x.Specialization);
        }

        public PsychologistSpecification(int psychologistId) : base(x => x.Id == psychologistId)
        {
            AddInclude(x => x.Feedbacks);
            AddInclude(x => x.Specialization);
        }

        public PsychologistSpecification(PsychologistSpecParams specParams) : base(
            x =>
            (string.IsNullOrEmpty(specParams.SearchName) || x.FullName.ToLower().Contains(specParams.SearchName.ToLower())))
        {
            AddInclude(x => x.Feedbacks);
            AddInclude(x => x.Specialization);
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    }
}
