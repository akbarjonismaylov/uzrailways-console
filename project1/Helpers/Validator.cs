using System.Security.Cryptography.X509Certificates;

namespace project1.Helpers;

public class Validator
{
    public static bool IsValidPinfl(string pinfl)
    {
        if (pinfl.Length != 14) return false;

        foreach (char c in pinfl)
        {
            if (!char.IsDigit(c)) return false;
        }
        return true;
    }

    public static bool IsValidDocumentSeries(string series)
    {
        if (series.Length != 2) return false;

        foreach (var s in series)
        {
            if (!char.IsUpper(s)) return false;
        }
        return true;
    }

    public static bool IsValidDocumentNumber(string number)
    {
        if (number.Length != 7) return false;

        foreach (char n in number)
        {
            if (!char.IsDigit(n)) return false;
        }
        return true;
    }

    public static bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;
        return true;
    }
}
