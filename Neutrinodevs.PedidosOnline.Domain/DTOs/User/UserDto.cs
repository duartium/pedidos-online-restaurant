using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.User
{
    public class UserDto
    {
        [JsonProperty("id_client")]
        public int IdClient { get; set; }

        [JsonProperty("identification")]
        public string Identification { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("client_type")]
        public int ClientType { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonIgnore]
        public string CodeEmailVerification { get; set; }
    }
}
