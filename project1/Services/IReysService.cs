using project1.Models;

namespace project1.Services
{
    public interface IReysService
    {
        List<Reys> GetAll();
        void Save(List<Reys> reyslar);
        Reys? FindById(List<Reys> reyslar, Guid id);
    }
}