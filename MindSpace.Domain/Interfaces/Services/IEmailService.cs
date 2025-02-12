using System.Collections.Generic;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlContent);
        Task SendEmailAsync(List<string> to, string subject, string htmlContent);

    }
}