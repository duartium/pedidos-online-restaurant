using Neutrinodevs.PedidosOnline.Domain.DTOs.Customer;
using Neutrinodevs.PedidosOnline.Domain.DTOs.Product;
using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Pos
{
    public class PosSaleDto
    {
        [JsonProperty("customer")]
        public CustomerDTO Customer { get; set; }
        
        [JsonProperty("products")]
        public ProductSaleDTO[] ProductIds { get; set; }

        [JsonProperty("subtotal")]
        public string Subtotal { get; set; }

        [JsonProperty("iva")]
        public string Iva { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }
    }
}
