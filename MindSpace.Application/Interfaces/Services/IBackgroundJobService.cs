
using Quartz;

namespace MindSpace.Application.Interfaces.Services
{
    public interface IBackgroundJobService
    {
        public Task ScheduleJobWithFireOnce<T>(string referenceId, int minutesFromNow) where T : IJob;
    }
}