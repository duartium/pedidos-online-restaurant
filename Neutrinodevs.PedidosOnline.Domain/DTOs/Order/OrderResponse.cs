using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Order
{
    public class OrderResponse
    {
        [JsonProperty("id_order")]
        public int IdOrder { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
