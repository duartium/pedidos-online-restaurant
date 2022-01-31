using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Customer
{
    public class CustomerDTO
    {
        [JsonProperty("identification")]
        public string Identification { get; set; }
        
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("id_client")]
        public int IdClient { get; set; }
    }
}
