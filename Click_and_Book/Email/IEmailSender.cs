using System.Threading.Tasks;

namespace Click_and_Book.Email
{
    public interface IEmailSender
    {
        Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details);
    }
}