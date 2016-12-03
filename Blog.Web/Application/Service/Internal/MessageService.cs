using System.Net.Mail;

namespace Blog.Web.Application.Service.Internal
{
    public class MessageService : IMessageService
    {
        public void SendEmail(MailMessage mailMessage)
        {
            new SmtpClient().Send(mailMessage);
        }
    }
}