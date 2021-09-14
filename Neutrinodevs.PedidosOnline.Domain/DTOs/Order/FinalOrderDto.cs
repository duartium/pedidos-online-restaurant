using Newtonsoft.Json;
using System.Collections.Generic;

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

        [JsonProperty("delivery_time")]
        public string DeliveryTime { get; set; }

        [JsonProperty("items")]
        public List<Item> items { get; set; }

        [JsonProperty("subtotal")]
        public decimal subtotal { get; set; }

        [JsonProperty("total")]
        public decimal total { get; set; }
    }
}
