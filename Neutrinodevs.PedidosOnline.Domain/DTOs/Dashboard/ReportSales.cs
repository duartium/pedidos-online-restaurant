using Newtonsoft.Json;
using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Dashboard
{
    public class ReportSales
    {
        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("successful_deliveries")]
        public int SuccessfulDeliveries { get; set; }

        [JsonProperty("items")]
        public List<ItemSale> Items { get; set; }
    }

    public class ItemSale
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("user_delivery")]
        public string UserDelivery { get; set; }

        [JsonProperty("total_item")]
        public decimal Total { get; set; }

        [JsonProperty("sale_type")]
        public string TipoVenta { get; set; }
    }
}
