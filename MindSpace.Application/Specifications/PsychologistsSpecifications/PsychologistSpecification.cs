using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using System.Security.Cryptography.Xml;

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
            (string.IsNullOrEmpty(specParams.SearchName) || x.FullName.ToLower().Contains(specParams.SearchName.ToLower()))
            && (specParams.SpecializationId == null || x.SpecializationId == specParams.SpecializationId)
            && (specParams.SessionPriceFrom == null || x.SessionPrice >= specParams.SessionPriceFrom)
            && (specParams.SessionPriceTo == null || x.SessionPrice <= specParams.SessionPriceTo)
            && (specParams.RatingFrom == null || x.AverageRating >= specParams.RatingFrom)
            && (specParams.Status == "All" || x.Status == Enum.Parse<UserStatus>(specParams.Status))
            )
        {
            AddInclude(x => x.Feedbacks);
            AddInclude(x => x.Specialization);
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "ratingAsc":
                    AddOrderBy(x => x.AverageRating);
                    break;
                case "ratingDesc":
                    AddOrderByDescending(x => x.AverageRating);
                    break;
                case "priceAsc":
                    AddOrderBy(x => x.SessionPrice);
                    break;
                case "priceDesc":
                    AddOrderByDescending(x => x.SessionPrice);
                    break;
            }
        }
    }
}
