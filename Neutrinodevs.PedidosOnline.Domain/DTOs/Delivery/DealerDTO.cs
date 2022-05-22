using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery
{
    public class DealerDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("full_names")]
        public string FullNames { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("activity_state")]
        public string ActivityState { get; set; }
    }
}
