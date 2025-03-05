using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Specifications.ApplicationUserSpecifications
{
    public class ApplicationUserSpecification : BaseSpecification<ApplicationUser>
    {
        /// <summary>
        /// Search By User Id
        /// </summary>
        /// <param name="id"></param>
        public ApplicationUserSpecification(int userId) : base(x => x.Id.Equals(userId))
        {
        }

        /// <summary>
        /// Search By email
        /// </summary>
        /// <param name="id"></param>
        public ApplicationUserSpecification(string email) : base(x => x.Email.Equals(email))
        {
        }

        /// <summary>
        /// Search By General Filter
        /// </summary>
        /// <param name="specParams"></param>
        /// <param name="isOnlyStudent">If true, only return student users</param>
        public ApplicationUserSpecification(ApplicationUserSpecParams specParams, bool isOnlyStudent = false) :
            base(s =>
                (string.IsNullOrEmpty(specParams.SearchName) ||
                s.Email!.ToLower().Contains(specParams.SearchName.ToLower()) ||
                s.UserName!.ToLower().Contains(specParams.SearchName.ToLower()) ||
                s.FullName!.ToLower().Contains(specParams.SearchName.ToLower())) &&
                (specParams.Status!.Equals("All") || s.Status == Enum.Parse<UserStatus>(specParams.Status)) &&
                (!specParams.MinAge.HasValue || DateTime.Today.AddYears(-specParams.MinAge.Value) >= s.DateOfBirth) &&
                (!specParams.MaxAge.HasValue || DateTime.Today.AddYears(-specParams.MaxAge.Value - 1) <= s.DateOfBirth) &&
                (!isOnlyStudent || s.Student != null) &&
                (s.Psychologist != null || s.Student != null || s.SchoolManager != null || s.Parent != null)
            && (!specParams.SchoolId.HasValue || s.SchoolManager.SchoolId == specParams.SchoolId || s.Student.SchoolId == specParams.SchoolId)// this ensures admin is not selected 
            )
        {

            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);


            // Add Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "dobAsc":
                        AddOrderBy(x => x.DateOfBirth.ToString()!); break;
                    case "dobDesc":
                        AddOrderByDescending(x => x.DateOfBirth.ToString()!); break;
                    case "nameAsc":
                        AddOrderBy(x => x.FullName!); break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.FullName!); break;
                }
            }
        }
    }
}