
using Quartz;

namespace MindSpace.Application.Interfaces.Services
{
    public interface IBackgroundJobService
    {
        public Task ScheduleJobWithFireOnce<T>(string referenceId,
            int minutesFromNow,
            Dictionary<string, object> jobDatas = null) where T : IJob;

        public Task ScheduleJobWithFireOnce<T>(string referenceId,
            DateTime dateTime,
            Dictionary<string, object> jobDatas = null) where T : IJob;
    }
}