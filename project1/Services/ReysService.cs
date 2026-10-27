using project1.Models;
using System.Text.Json;

namespace project1.Services;

public class ReysService : IReysService

{
    public List<Reys> GetAll()
    {
        List<Reys> reyslar = new List<Reys>();

        if (File.Exists("reyslar.json"))
        {
            string reysJson = File.ReadAllText("reyslar.json");
            List<Reys>? readReys = JsonSerializer.Deserialize<List<Reys>>(reysJson);

            if (readReys != null)
            {
                reyslar = readReys;
            }
        }

        else
        {
            reyslar.Add(new Reys
            {
                Id = Guid.NewGuid(),
                From = "Toshkent",
                To = "Moskva",
                DepartureTime = new DateTime(2026, 10, 20, 12, 0, 0),
                ArrivalTime = new DateTime(2026, 10, 23, 9, 5, 0),
                Price = 1200000,
                TotalSeats = 55,
                AvailableSeats = 32
            });

            reyslar.Add(new Reys
            {
                Id = Guid.NewGuid(),
                From = "Toshkent",
                To = "Almaty",
                DepartureTime = new DateTime(2026, 10, 20, 18, 0, 0),
                ArrivalTime = new DateTime(2026, 10, 21, 1, 0, 0),
                Price = 600000,
                TotalSeats = 55,
                AvailableSeats = 22
            });

            reyslar.Add(new Reys
            {
                Id = Guid.NewGuid(),
                From = "Toshkent",
                To = "Baku",
                DepartureTime = new DateTime(2026, 10, 21, 1, 30, 0),
                ArrivalTime = new DateTime(2026, 10, 23, 13, 47, 0),
                Price = 1800000,
                TotalSeats = 55,
                AvailableSeats = 37
            });

            reyslar.Add(new Reys
            {
                Id = Guid.NewGuid(),
                From = "Toshkent",
                To = "Dubay",
                DepartureTime = new DateTime(2026, 10, 22, 16, 0, 0),
                ArrivalTime = new DateTime(2026, 10, 24, 17, 0, 0),
                Price = 2500000,
                TotalSeats = 55,
                AvailableSeats = 44
            });
        }
        return reyslar;
    }

    public void Save(List<Reys> reyslar)
    {
        string writeReys = JsonSerializer.Serialize(reyslar);
        File.WriteAllText("reyslar.json", writeReys);
    }

    public Reys? FindById(List<Reys> reyslar, Guid id)
    {
        for (int i = 0; i < reyslar.Count; i++)
        {
            if (reyslar[i].Id == id)
            {
                return reyslar[i];
            }
        }
        return null;
    }
}


