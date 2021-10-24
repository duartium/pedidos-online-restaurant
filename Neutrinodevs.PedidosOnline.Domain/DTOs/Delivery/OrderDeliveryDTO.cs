using Neutrinodevs.PedidosOnline.Domain.DTOs.Order;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery
{
    public class OrderDeliveryDTO
    {
        [JsonProperty("id_order")]
        public int IdOrder { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("cellphone_number")]
        public string CellphoneNumber { get; set; }

        [JsonProperty("delivery_time")]
        public string DeliveryTime { get; set; }

        [JsonProperty("items")]
        public string JsonProducts { get; set; }

        [JsonProperty("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }
    }
}
