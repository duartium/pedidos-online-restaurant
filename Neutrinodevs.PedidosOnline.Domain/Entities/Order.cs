﻿using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.Entities
{
    public class Order
    {
        [JsonProperty("id_order")]
        public int IdOrder { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonIgnore]
        public string CustomerName { get; set; }

        [JsonProperty("date")]
        public string DateTime { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("id_stage")]
        public int IdStage { get; set; }

        [JsonProperty("stage")]
        public string Stage { get; set; }
    }
}
