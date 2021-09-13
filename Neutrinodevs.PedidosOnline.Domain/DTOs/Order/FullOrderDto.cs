using System.Collections.Generic;

namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Order
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }

    public class FullOrderDto
    {
        public int id_client { get; set; }
        public List<Item> items { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
    }
}
