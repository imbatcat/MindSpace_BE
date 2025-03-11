namespace MindSpace.Application.Interfaces.Services.EmailServices
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email to a single recipient
        /// </summary>
        /// <param name="to">Email address of the recipient</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="htmlContent">HTML content of the email body</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task SendEmailAsync(string to, string subject, string htmlContent);

        /// <summary>
        /// Sends an email to multiple recipients
        /// </summary>
        /// <param name="to">List of email addresses of the recipients</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="htmlContent">HTML content of the email body</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task SendEmailAsync(List<string> to, string subject, string htmlContent);
    }
}