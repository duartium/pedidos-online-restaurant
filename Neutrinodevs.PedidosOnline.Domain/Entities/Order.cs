namespace Neutrinodevs.PedidosOnline.Domain.Entities
{
    public class Order
    {
        public int IdOrder { get; set; }
        public int Number { get; set; }
        public string CustomerName { get; set; }
        public string DateTime { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
