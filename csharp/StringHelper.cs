using System.Text;
using System.Text.RegularExpressions;

namespace UnitTesting;

public class StringHelper
{
    public string Truncate(string text, int maxLength, string suffix = "...")
    {
        if (maxLength <= 0)
            throw new ArgumentException("maxLength debe ser mayor a 0.");
        if (text.Length <= maxLength)
            return text;
        return text[..maxLength] + suffix;
    }

    public string ToSlug(string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;

        // Normalizar y eliminar tildes
        string normalized = text.Normalize(NormalizationForm.FormD);
        string withoutAccents = Regex.Replace(normalized, @"\p{Mn}", "");

        return withoutAccents
            .ToLower()
            .Replace("ñ", "n")
            .Trim()
            .Replace(" ", "-")
            .Pipe(s => Regex.Replace(s, @"[^a-z0-9\-]", ""))
            .Pipe(s => Regex.Replace(s, @"-+", "-"));
    }

    public int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }
}

// Extensión helper para encadenar Regex.Replace de forma fluida
public static class StringExtensions
{
    public static string Pipe(this string s, Func<string, string> fn) => fn(s);
}
