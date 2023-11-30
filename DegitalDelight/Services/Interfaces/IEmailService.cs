namespace DegitalDelight.Services.Interfaces
{
    public interface IEmailService
    {
        void SendMail(string email, string subject, string body);
    }
}
