using project1.Models;
using System.Text.Json;

namespace project1.Services;

public class TicketService : ITicketService
{
    public Ticket Create(User user, Reys reys)
    {
        Ticket ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            ReysId = reys.Id,
            Price = reys.Price,
            PurchasedAt = DateTime.Now
        };
        return ticket;
    }

    public List<Ticket> GetAll()
    {
        List<Ticket> tickets = new List<Ticket>();

        if (File.Exists("tickets.json"))
        {
            string ticketJson = File.ReadAllText("tickets.json");
            List<Ticket>? readTicket = JsonSerializer.Deserialize<List<Ticket>>(ticketJson);
            if (readTicket != null)
            {
                tickets = readTicket;
            }
        }
        return tickets;
    }
    public void Save(List<Ticket> tickets)
    {
        string writeTicket = JsonSerializer.Serialize(tickets);
        File.WriteAllText("tickets.json", writeTicket);
    }
}
