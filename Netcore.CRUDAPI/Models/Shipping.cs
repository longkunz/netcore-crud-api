namespace Netcore.CRUDAPI.Models;

public class Shipping
{
	public int Id { get; set; }
    public int OrderId { get; set; }
    public string ShippingMethod { get; set; }
	public decimal ShippingCost { get; set; }
}
