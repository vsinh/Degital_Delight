using DegitalDelight.Services.Interfaces;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;

namespace DegitalDelight.Services
{
    public class EmailService : IEmailService
    {
        MailboxAddress mailboxAddress;
        public EmailService()
        {
            mailboxAddress = MailboxAddress.Parse("chauvinhsinh12@gmail.com");
        }
        public void SendMail(string email, string subject, string body)
        {
            var mail = new MimeMessage();
            mail.From.Add(mailboxAddress);
            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject = subject;
            mail.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("chauvinhsinh12@gmail.com", "wosb gijh qikz juec");
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
