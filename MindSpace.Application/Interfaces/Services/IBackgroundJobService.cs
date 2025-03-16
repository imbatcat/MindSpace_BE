
using Quartz;

namespace MindSpace.Application.Interfaces.Services
{
    public interface IBackgroundJobService
    {
        public Task ScheduleJobWithFireOnce<T>(string referenceId, int minutesFromNow) where T : IJob;

        public Task ScheuleJobWithFireOnDate<T>(string referenceId, TimeSpan timeSpan) where T : IJob;
    }
}