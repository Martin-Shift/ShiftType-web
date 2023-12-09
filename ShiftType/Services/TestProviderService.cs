using ShiftType.DbModels;
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
       public static Quote GetRandomQuote(string language, QuoteType quoteType, TypingDbContext context)
        {
            IEnumerable<Quote> quotes = context.Quotes;
            //TODO make quote languages
            // = context.Quotes.Where(x => x.Language == language);
            switch (quoteType)
            {
                case QuoteType.All:
                    break;
                case QuoteType.Short:
                    quotes = context.Quotes.Where(x => x.Text.Length <= 105);
                    break;
                case QuoteType.Medium:
                    quotes = context.Quotes.Where(x => x.Text.Length > 105 && x.Text.Length <= 275);
                    break;
                case QuoteType.Large:
                    quotes = context.Quotes.Where(x => x.Text.Length > 275 && x.Text.Length <= 550);
                    break;
                case QuoteType.Thicc:
                    quotes = context.Quotes.Where(x => x.Text.Length > 550);
                    break;

            }
            return quotes.ElementAt(new Random().Next(0, quotes.Count()));

        }
    }
}
