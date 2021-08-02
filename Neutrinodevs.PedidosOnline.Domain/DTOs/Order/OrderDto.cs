using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Order
{
    public class OrderDto
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

        public OrderDto()
        {
            IdOrder = 0;
            Number = 0;
            CustomerName = string.Empty;
            DateTime = string.Empty;
            TotalAmount = decimal.Zero;
        }
    }
}
