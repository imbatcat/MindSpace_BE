namespace MindSpace.Application.Interfaces.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlContent);
        Task SendEmailAsync(List<string> to, string subject, string htmlContent);

    }
}