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

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("names")]
        public string Names { get; set; }

        [JsonProperty("surnames")]
        public string Surnames { get; set; }
    }

    public class CustomerSaveDTO
    {
        [JsonProperty("identification")]
        public string Identification { get; set; }

        [JsonProperty("name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class CustomerUpdateDto
    {
        [JsonProperty("identification")]
        public string Identification { get; set; }

        [JsonProperty("id_client")]
        public int IdClient { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("names")]
        public string Names { get; set; }

        [JsonProperty("surnames")]
        public string Surnames { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
