using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.Application.Services
{
    public class SupportingProgramService : ISupportingProgramService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupportingProgramService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Count number of supporting program under the spec
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<int> CountSupportingProgramAsync(ISpecification<SupportingProgram> spec)
        {
            return await _unitOfWork.Repository<SupportingProgram>().CountAsync(spec);
        }

        public Task<SupportingProgram?> CreateSuppportingProgramAsync()
        {
            return null;
        }

        public async Task<SupportingProgram?> DeleteSuppportingProgramAsync(SupportingProgram sp)
        {
            var deletedSupportingProgram = _unitOfWork.Repository<SupportingProgram>().Delete(sp);
            await _unitOfWork.CompleteAsync();
            return deletedSupportingProgram;
        }

        public async Task<IReadOnlyList<SupportingProgram>> GetAllSupportingProgramAsync(ISpecification<SupportingProgram> spec)
        {
            return await _unitOfWork.Repository<SupportingProgram>().GetAllWithSpecAsync(spec);
        }

        public Task<IReadOnlyList<SupportingProgram>> GetAllSupportingProgramFromDateRange(DateTime fromDateTime, DateTime toDateTime)
        {
            return null;
        }

        public async Task<SupportingProgram?> UpdateSupportingProgramAsync(SupportingProgram sp)
        {
            var updatedSupportingProgram = _unitOfWork.Repository<SupportingProgram>().Update(sp);
            await _unitOfWork.CompleteAsync();
            return updatedSupportingProgram;
        }
    }
}
