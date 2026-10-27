namespace project1.Models;

public class Reys
{
    public Guid Id { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal Price { get; set; }
    public int TotalSeats { get; set; }
    public int AvailableSeats { get; set; }
}
