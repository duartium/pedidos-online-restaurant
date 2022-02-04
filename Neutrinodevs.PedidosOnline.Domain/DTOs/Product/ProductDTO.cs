using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Product
{
    public class ProductDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Nombre { get; set; }

        [JsonProperty("price")]
        public decimal Precio { get; set; }

        [JsonProperty("image")]
        public string Imagen { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string JsonProduct { get; set; }
    }

    public class ProductSaleDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("full_value")]
        public string full_value { get; set; }
    }
}
