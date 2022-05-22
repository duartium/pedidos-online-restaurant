namespace Neutrinodevs.PedidosOnline.Domain.DTOs.Delivery
{
    public class AssignDeliveryDTO
    {
        public int IdOrder { get; set; }
        public int IdEmployee { get; set; }
    }

    public class AssignDealerDTO
    {
        public int IdOrder { get; set; }
        public int IdDealer { get; set; }
    }
}
