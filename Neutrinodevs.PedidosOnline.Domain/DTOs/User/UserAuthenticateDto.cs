﻿using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.User
{
    public class UserAuthenticateDto
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("id_client")]
        public int IdClient { get; set; }

        [JsonProperty("id_user")]
        public int IdUser { get; set; }

        [JsonProperty("id_role")]
        public int IdRole { get; set; }
    }

}
