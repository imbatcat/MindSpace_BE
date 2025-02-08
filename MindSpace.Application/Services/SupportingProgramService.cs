using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.Application.Services
{
    public class SupportingProgramService : ISupportingProgramService
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly IUnitOfWork _unitOfWork;

        // ================================
        // === Constructors
        // ================================

        public SupportingProgramService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<int> CountSupportingProgramAsync(ISpecification<SupportingProgram> spec)
        {
            return await _unitOfWork.Repository<SupportingProgram>().CountAsync(spec);
        }

        public async Task<IReadOnlyList<SupportingProgram>> GetAllSupportingProgramAsync(ISpecification<SupportingProgram> spec)
        {
            return await _unitOfWork.Repository<SupportingProgram>().GetAllWithSpecAsync(spec);
        }

        public async Task<SupportingProgram?> GetSupportingProgramByIdAsync(int id)
        {
            return await _unitOfWork.Repository<SupportingProgram>().GetByIdAsync(id);
        }
    }
}
