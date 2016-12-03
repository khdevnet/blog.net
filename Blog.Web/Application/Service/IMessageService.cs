using System.Net.Mail;

namespace Blog.Web.Application.Service
{
    public interface IMessageService
    {
        void SendEmail(MailMessage mailMessage);
    }
}