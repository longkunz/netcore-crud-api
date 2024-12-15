namespace Netcore.CRUDAPI.Dtos;

public class ShippingDto
{
	public int Id { get; set; }
	public int OrderId { get; set; }
	public string ShippingMethod { get; set; }
	public decimal ShippingCost { get; set; }
}
