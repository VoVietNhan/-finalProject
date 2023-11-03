using Common.Utils;

namespace ServiceAuthentication.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
