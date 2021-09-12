using Newtonsoft.Json;
using System;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.User
{
    public class UserRegisterDto
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("id_client")]
        public int IdClient { get; set; }
    }
}
