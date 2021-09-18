using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.User
{
    public class UserAuthenticateDto
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("id_client")]
        public int IdClient { get; set; }
    }

}
