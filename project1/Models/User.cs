namespace project1.Models;

public class User
{
    public Guid Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? Patronymic { get; set; }
    public string DocumentSeries { get; set; }
    public string DocumentNumber { get; set; }
    public string Pinfl { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; }

}
