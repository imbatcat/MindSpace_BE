using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface ISupportingProgramService
    {
        public Task<int> CountSupportingProgramAsync(ISpecification<SupportingProgram> spec);
        public Task<IReadOnlyList<SupportingProgram>> GetAllSupportingProgramAsync(ISpecification<SupportingProgram> spec);
    }
}
