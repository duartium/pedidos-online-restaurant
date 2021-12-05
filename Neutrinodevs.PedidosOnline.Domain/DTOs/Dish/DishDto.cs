using Newtonsoft.Json;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Dish
{
    public class DishDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("image_name")]
        public string ImageName { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }
    }
}
