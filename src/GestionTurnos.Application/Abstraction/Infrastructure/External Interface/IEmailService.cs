using GestionTurnos.Application.Response;
using System.Threading.Tasks;

namespace GestionTurnos.Application.Abstraction.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}