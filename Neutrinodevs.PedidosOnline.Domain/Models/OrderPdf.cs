using System;
using System.Collections.Generic;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Domain.Models
{
    public class OrderPdf
    {
        public string OrderNumber { get; set; }
        public string CustomerFullname { get; set; }
        public string Dealer { get; set; }

    }
}
