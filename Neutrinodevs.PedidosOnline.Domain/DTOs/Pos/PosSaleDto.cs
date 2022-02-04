using Neutrinodevs.PedidosOnline.Domain.DTOs.Product;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Pos
{
    public class PosSaleDto
    {
        [JsonProperty(PropertyName = "id_client")]
        public int id_client { get; set; }
        
        [JsonPropertyName("products")]
        public ProductSaleDTO[] Products { get; set; }

        [JsonPropertyName("subtotal")]
        public string Subtotal { get; set; }

        [JsonPropertyName("iva")]
        public string Iva { get; set; }

        [JsonPropertyName("total")]
        public string Total { get; set; }
    }
}
