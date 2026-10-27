using project1.Dtos;
using project1.Models;

namespace project1.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        void Save(List<User> users);
        User? FindByPinfl(List<User> users, string pinfl);
        User? FindById(List<User> users, Guid id);
        User Create(UserCreateDto dto);
    }
}