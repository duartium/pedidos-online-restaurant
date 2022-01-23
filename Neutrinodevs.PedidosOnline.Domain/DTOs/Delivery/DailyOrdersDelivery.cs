using Newtonsoft.Json;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery
{
    public class DailyOrdersDelivery
    {
        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("total_products")]
        public int TotalProducts { get; set; }

        [JsonProperty("successful_deliveries")]
        public int SuccessfulDeliveries { get; set; }

        [JsonProperty("items")]
        public List<ItemDailyOrderDelivery> Items { get; set; }
    }

    public class ItemDailyOrderDelivery
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("total_item")]
        public decimal Total { get; set; }
    }
}
