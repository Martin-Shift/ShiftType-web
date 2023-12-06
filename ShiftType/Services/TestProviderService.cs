using ShiftType.Models;
using System.Text.Json;

namespace ShiftType.Services
{
    public static class TestProviderService
    {
        public static string[] GetWords(string language)
        {
            var random = new Random();
            var dir = "D:\\Mein progectos\\ShiftType\\ShiftType\\bin\\debug\\net6.0\\";
            var path = $"Words/{language.Replace(" ", "_")}.json";
            var text = System.IO.File.ReadAllText(dir + path);
            return JsonSerializer.Deserialize<string[]>(text);
        }
        public static string GetRandomQuote(string language, QuoteType quoteType)
        {
            var quotes = JsonSerializer.Deserialize<string[]>(System.IO.File.ReadAllText($"Quotes{language.Replace(" ", "_")}.json"));
            switch (quoteType)
            {
                case QuoteType.All:
                    break;
                case QuoteType.Short:
                    quotes = quotes.Where(x => x.Length <= 105).ToArray();
                    break;
                case QuoteType.Medium:
                    quotes = quotes.Where(x => x.Length > 105 && x.Length <= 275).ToArray();
                    break;
                case QuoteType.Large:
                    quotes = quotes.Where(x => x.Length > 275 && x.Length <= 550).ToArray();
                    break;
                case QuoteType.Thicc:
                    quotes = quotes.Where(x => x.Length > 550).ToArray();
                    break;

            }
            return quotes[new Random().Next(0, quotes.Length - 1)];

        }
    }
}
