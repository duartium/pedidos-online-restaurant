using System.Text.Json.Serialization;

namespace Neutrinodevs.PedidosOnline.Domain.Models
{
    public class CancelOrder
    {
        [JsonPropertyName("id_order")]
        public int IdOrder { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        [JsonPropertyName("stage")]
        public int IdStage { get; set; }
    }
}
