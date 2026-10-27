namespace project1.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ReysId { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchasedAt { get; set; }
}
