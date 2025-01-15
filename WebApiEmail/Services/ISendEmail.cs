using System.Threading.Tasks;

namespace WebApiEmail.Services
{
    public interface ISendEmail
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
