namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Order
{
    public class OrderResumeDTO
    {
        public string CustomerEmail { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
    }
}
