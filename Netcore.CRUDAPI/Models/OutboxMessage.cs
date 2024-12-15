namespace Netcore.CRUDAPI.Models;

public class OutboxMessage
{
	public int Id { get; set; }
	public string Type { get; set; } // Loại message (ví dụ: "CreateShipping")
	public string Data { get; set; } // Dữ liệu message (ví dụ: JSON của ShippingDto)
	public DateTime CreatedAt { get; set; }
	public bool Processed { get; set; }
}
