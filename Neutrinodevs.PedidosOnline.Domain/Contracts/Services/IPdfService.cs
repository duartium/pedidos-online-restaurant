using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using System;

namespace ND.EDUC_CONTINUA.DOMAIN.Contracts.Services
{
    public interface IPdfService
    {
        Byte[] Generate(FinalOrderDto data);
    }
}