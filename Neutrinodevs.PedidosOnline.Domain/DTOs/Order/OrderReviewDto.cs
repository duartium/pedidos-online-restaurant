using Newtonsoft.Json;
using System;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Order
{
    [Serializable]
    public class OrderReviewDto
    {
        [JsonProperty("id_order")]
        public int IdOrder { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("stage")]
        public string Stage { get; set; }

        [JsonProperty("dealer")]
        public string Dealer { get; set; }
    }
}
