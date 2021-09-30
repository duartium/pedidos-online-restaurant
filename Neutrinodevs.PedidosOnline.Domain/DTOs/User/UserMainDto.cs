using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.User
{
    public class UserMainDto
    {
        [JsonProperty("id_user")]
        public int IdUsuario { get; set; }

        [JsonProperty("names")]
        public string Nombres { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("user_type")]
        public string TipoUsuario { get; set; }
    }
}
