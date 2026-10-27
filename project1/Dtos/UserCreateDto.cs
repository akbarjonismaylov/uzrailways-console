using System.Globalization;

namespace project1.Dtos;

public class UserCreateDto
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? Patronymic { get; set; }
    public string DocumentSeries { get; set; }
    public string DocumentNumber { get; set; }
    public string Pinfl { get; set; }
    public DateTime Birthday  { get; set; }
}
