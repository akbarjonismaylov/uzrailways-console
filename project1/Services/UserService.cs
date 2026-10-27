using project1.Dtos;
using project1.Models;
using System.Text.Json;

namespace project1.Services;

public class UserService : IUserService
{
    public List<User> GetAll()
    {
        List<User> users = new List<User>();

        if (File.Exists("users.json"))
        {
            string userJson = File.ReadAllText("users.json");
            List<User>? userRead = JsonSerializer.Deserialize<List<User>>(userJson);
            if (userRead != null)
            {
                users = userRead;
            }
        }
        return users;
    }

    public void Save(List<User> users)
    {
        string writeUser = JsonSerializer.Serialize(users);
        File.WriteAllText("users.json", writeUser);
    }
    public User? FindByPinfl(List<User> users, string pinfl)
    {
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Pinfl == pinfl)
            {
                return users[i];
            }
        }
        return null;
    }

    public User? FindById(List<User> users, Guid id)
    {
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Id == id)
            {
                return users[i];
            }
        }
        return null;
    }

    public User Create(UserCreateDto dto)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Birthday = dto.Birthday,
            DocumentSeries = dto.DocumentSeries,
            DocumentNumber = dto.DocumentNumber,
            Pinfl = dto.Pinfl,
            CreatedAt = DateTime.Now
        };
        return user;
    }

}