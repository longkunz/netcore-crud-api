namespace Netcore.CRUDAPI.Dtos;

public class OrderDto
{
	public int Id { get; set; }
	public DateTime OrderDate { get; set; }
	public string CustomerName { get; set; }
	public string CustomerAddress { get; set; }
	public decimal TotalAmount { get; set; }
	public List<OrderItemDto> OrderItems { get; set; }
}
