using project1.Dtos;
using project1.Helpers;
using project1.Models;
using project1.Services;

namespace project1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IReysService reysService = new ReysService();
            List<Reys> reyslar = reysService.GetAll();

            ITicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetAll();

            IUserService userService = new UserService();
            List<User> users = userService.GetAll();

            while (true)
            {
                for (int i = 0; i < reyslar.Count; i++)
                {
                    Reys reys = reyslar[i];
                    Console.WriteLine($"{i + 1}. {reys.From} => {reys.To} | {reys.DepartureTime:dd.MM.yyyy HH:mm} | {reys.ArrivalTime:dd.MM.yyyy HH:mm} | Joylar {reys.TotalSeats} | Bo'sh joylar {reys.AvailableSeats} | Summa => {reys.Price}");
                }

                Console.Write("Reysni tanlang (Chiqish uchun 0 | Chiptalarim uchun 9 | Chiptalarni bekor qilish uchun 8 ) => ");
                string? selectedFlight = Console.ReadLine();

                bool right = int.TryParse(selectedFlight, out int choose);

                if (right && choose == 0)
                {
                    break;
                }

                if (right && choose == 8)
                {
                    if (tickets.Count == 0)
                    {
                        Console.WriteLine("\nHali chipta olinmagan.\n");
                        continue;
                    }


                    Console.WriteLine("\nSizning chiptalaringiz:");

                    ShowTickets(tickets, reyslar, reysService, users, userService);

                    Console.Write("Bekor qilinadigan chipta raqami (Chiqish uchun 0) > ");

                    string? cansele = Console.ReadLine();
                    bool which = int.TryParse(cansele, out int chooseCansele);

                    if (!which)
                    {
                        Console.WriteLine("Son kiriting!");
                        continue;
                    }

                    if (chooseCansele == 0)
                    {
                        Console.WriteLine();
                        continue;
                    }

                    if (chooseCansele < 1 || chooseCansele > tickets.Count)
                    {
                        Console.WriteLine("Bunday chipta yo'q!");
                        continue;
                    }

                    Ticket tanlanganChipta = tickets[chooseCansele - 1];

                    Reys? topilganflight = reysService.FindById(reyslar, tanlanganChipta.ReysId);

                    tickets.Remove(tanlanganChipta);
                    ticketService.Save(tickets);

                    if (topilganflight != null)
                    {
                        Console.WriteLine($"Tanlangan chipta: {topilganflight.From} -> {topilganflight.To}");
                        topilganflight.AvailableSeats++;
                        reysService.Save(reyslar);
                        Console.WriteLine($"{topilganflight.From} -> {topilganflight.To} chipta o'chirildi.");
                    }

                    Console.WriteLine();
                    continue;
                }

                if (right && choose == 9)
                {
                    if (tickets.Count == 0)
                    {
                        Console.WriteLine("\nHali chipta olinmagan.\n");
                        continue;
                    }

                    Console.WriteLine("\nSizning chiptalaringiz:");

                    ShowTickets(tickets, reyslar, reysService, users, userService);

                    Console.WriteLine();
                    continue;
                }

                if (right && choose >= 1 && choose <= reyslar.Count)
                {
                    Reys reys = reyslar[choose - 1];




                    if (reys.AvailableSeats > 0)
                    {

                        Console.WriteLine($"\nTanlangan reys: {reys.From} => {reys.To} | {reys.DepartureTime:dd.MM.yyyy HH:mm} | Summa => {reys.Price}");

                        string lastName = AskName("Familiya");

                        string firstName = AskName("Ism");

                        string documentSeries = AskSeries();

                        string documentNumber = AskNumber();

                        string pinfl = AskPinfl();

                        DateTime chooseBirthday = AskBirthday();

                        UserCreateDto dto = new UserCreateDto
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            DocumentSeries = documentSeries,
                            DocumentNumber = documentNumber,
                            Pinfl = pinfl,
                            Birthday = chooseBirthday
                        };

                        User? user = userService.FindByPinfl(users, dto.Pinfl);

                        if (user == null)
                        {
                            user = userService.Create(dto);
                            users.Add(user);
                        }

                        Ticket ticket = ticketService.Create(user, reys);

                        reys.AvailableSeats--;
                        tickets.Add(ticket);

                        reysService.Save(reyslar);
                        ticketService.Save(tickets);
                        userService.Save(users);

                        Console.WriteLine();
                        Console.WriteLine("Chipta ma'lumotlari:");
                        Console.WriteLine($"Yo'lovchi: {user.LastName} {user.FirstName} | Tug'ulgan sanasi: {user.Birthday:dd.MM.yyyy}");
                        Console.WriteLine($"Yo'nalish: {reys.From} -> {reys.To}");
                        Console.WriteLine($"Jo'nash vaqti: {reys.DepartureTime:dd.MM.yyyy HH:mm}");
                        Console.WriteLine($"Narxi: {ticket.Price}");
                        Console.WriteLine($"Sotib olingan vaqt: {ticket.PurchasedAt:dd.MM.yyyy HH:mm}");
                        Console.WriteLine();
                    }

                    else
                    {
                        Console.WriteLine("\nBu reysda joy qolmagan!\n");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNoto'g'ri raqam, 1 dan {reyslar.Count} gacha kiriting.\n");
                }
            }
        }

        static void ShowTickets(List<Ticket> tickets, List<Reys> reyslar, IReysService reysService, List<User> users, IUserService userService)
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                Ticket showTicket = tickets[i];

                Reys? topilganReys = reysService.FindById(reyslar, showTicket.ReysId);
                string yonalish = "";

                if (topilganReys != null)
                {
                    yonalish = $"{topilganReys.From} -> {topilganReys.To}";
                }

                User? topilganUser = userService.FindById(users, showTicket.UserId);
                string yolovchi = "";

                if (topilganUser != null)
                {
                    yolovchi = $"{topilganUser.LastName} {topilganUser.FirstName}";
                }
                Console.WriteLine($"{i + 1}. Narxi: {showTicket.Price} | Yo'lovchi: {yolovchi} | Yo'nalish: {yonalish}");
            }
        }

        static string AskPinfl()
        {
            string pinfl;
            while (true)
            {
                Console.Write("PINFL > ");
                pinfl = (Console.ReadLine() ?? "").Trim();

                if (!Validator.IsValidPinfl(pinfl))
                {
                    Console.WriteLine("PINFL aniq 14 ta raqamdan iborat bo'lishi kerak!");
                    continue;
                }
                break;
            }
            return pinfl;
        }

        static string AskSeries()
        {
            string series;
            while (true)
            {
                Console.Write("Pasport seriyasi > ");
                series = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (!Validator.IsValidDocumentSeries(series))
                {
                    Console.WriteLine("Pasport seriyasi 2 ta bo'lishi kerak!");
                    continue;
                }
                break;
            }
            return series;
        }

        static string AskNumber()
        {
            string documentNumber;
            while (true)
            {
                Console.Write("Pasport raqami > ");
                documentNumber = (Console.ReadLine() ?? "").Trim();

                if (!Validator.IsValidDocumentNumber(documentNumber))
                {
                    Console.WriteLine("Pasport raqami 7 ta bo'lishi kerak!");
                    continue;
                }
                break;
            }
            return documentNumber;
        }

        static string AskName(string label)
        {
            string name;

            while (true)
            {
                Console.Write($"{label} > ");
                name = (Console.ReadLine() ?? "").Trim();

                if (!Validator.IsValidName(name))
                {
                    Console.WriteLine($"{label} bo'sh bo'lishi mumkin emas!");
                    continue;
                }
                break;
            }
            return name;
        }

        static DateTime AskBirthday()
        {
            DateTime chooseBirthday;
            while (true)    
            {

                Console.Write("Tug'ulgan sana (kk.oo.yyyy) > ");
                string? birthday = (Console.ReadLine() ?? "").Trim();

                bool date = DateTime.TryParse(birthday, out chooseBirthday);

                if (!date)
                {
                    Console.WriteLine("\nNoto'g'ri sana !\n");
                    continue;
                }

                if (chooseBirthday > DateTime.Now)
                {
                    Console.WriteLine("\nNoto'g'ri sana !\n");
                    continue;
                }
                break;
            }
            return chooseBirthday;
        }
    }
}