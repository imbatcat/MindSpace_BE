using FluentEmail.Core;
using MindSpace.Application.Interfaces.Services;

namespace MindSpace.Infrastructure.Services
{
    public class EmailSenderService(IFluentEmail fluentEmail) : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string htmlContent)
        {
            return fluentEmail.To(to).Subject(subject).Body(htmlContent, true).SendAsync();
        }

        public Task SendEmailAsync(List<string> to, string subject, string htmlContent)
        {
            return fluentEmail.To((IEnumerable<FluentEmail.Core.Models.Address>)to).Subject(subject).Body(htmlContent, true).SendAsync();
        }
    }
}