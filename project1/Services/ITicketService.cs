using project1.Models;

namespace project1.Services
{
    public interface ITicketService
    {
        List<Ticket> GetAll();
        void Save(List<Ticket> tickets);
        Ticket Create(User user, Reys reys );   
    }
}