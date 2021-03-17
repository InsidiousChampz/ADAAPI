namespace STANDARDAPI.DTOs.Order
{
    public class UpdateOrderStatusDto
    {
        public bool OrderStatus { get; set; }       // For Cancel Order.
        public bool OrderListStatus { get; set; }   // For Product Out Of Stock.
    }
}