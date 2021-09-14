using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Order
{
    public class FinalOrderDto
    {
        [JsonProperty("id_order")]
        public int IdOrder { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("date")]
        public string DateTime { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }
    }
}
