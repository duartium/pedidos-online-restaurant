using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Employee
{
    public class EmployeeDto
    {
        [JsonProperty("id_employee")]
        public int IdEmployee { get; set; }

        [JsonProperty("identification")]
        public string Identification { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("employee_type")]
        public string EmployeeType { get; set; }

        [JsonProperty("usuario")]
        public string Usuario { get; set; }
    }
}
