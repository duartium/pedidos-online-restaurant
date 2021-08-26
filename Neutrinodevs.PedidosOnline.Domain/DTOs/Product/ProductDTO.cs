using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Product
{
    public class ProductDTO
    {
        [JsonProperty("name")]
        public string Nombre { get; set; }

        [JsonProperty("price")]
        public decimal Precio { get; set; }

        [JsonProperty("image_url")]
        public string Imagen { get; set; }

        [JsonIgnore]
        public string JsonProduct { get; set; }
    }
}
