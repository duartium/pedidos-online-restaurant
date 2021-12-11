using Neutrinodevs.PedidosOnline.Domain.Models;

namespace Neutrinodevs.PedidosOnline.Domain.Contracts.Services
{
    public interface IEmailService
    {
        void Send(EmailParams emailParams, out string message);
    }
}
