
using OfficeOpenXml.Filter;
using Quartz;

namespace MindSpace.Application.Interfaces.Services
{
    public interface IBackgroundJobService
    {
        public Task ScheduleJobWithFireOnce<T>(string sessionId, int minutesFromNow) where T : IJob;
    }
}