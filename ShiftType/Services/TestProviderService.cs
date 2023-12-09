using Microsoft.CodeAnalysis;
using ShiftType.DbModels;
using ShiftType.Models;
using System;
using System.Text.Json;

namespace ShiftType.Services
{
    public static class TestProviderService
    {
        public static string GetWords(string language,int? wordCount, bool? isPunctuation, bool? isNumbers)
        {
            var random = new Random();
            var dir = "D:\\Mein progectos\\ShiftType\\ShiftType\\bin\\debug\\net6.0\\";
            var path = $"Words/{language.Replace(" ", "_")}.json";
            var text = System.IO.File.ReadAllText(dir + path);
            var words = JsonSerializer.Deserialize<List<string>>(text);
            
            if(isPunctuation ?? false)
            {
                var arr = words.Select(x => x.ToCharArray().ToList()).ToList();
                var symbols = "'\"!;:?-.,()";
                for(int i=0;i<words.Count / 3;i++)
                {
                    var next = random.Next(0, words.Count);
                    if (!symbols.Contains(arr[next].Last()))
                    {
                        var symbol = symbols[random.Next(0, symbols.Length)];
                        if("\"'()".Contains(symbol))
                        {
                            arr[next].Insert(0, symbol == ')' ? '(' : symbol);
                            arr[next].Add(symbol == '(' ? ')' : symbol);
                        }
                        else arr[next].Add(symbol);
                    }
                    var upperNext = random.Next(words.Count);
                    arr[upperNext][0] = char.ToUpper(arr[upperNext][0]);
                }
                words = arr.Select(x => string.Join("", x)).ToList();
            }
            if (isNumbers ?? false)
            {
                for (int i = 0; i < words.Count / 4; i++)
                {
                    words.Insert(random.Next(0, words.Count), random.Next(0, (int)Math.Pow(random.Next(1,10),random.Next(1,5))).ToString());
                }
            }
            var test = "";
            for (int i = 0; i <wordCount; i++)
            {
                test += words[random.Next(0, words.Count - 1)] + " ";
            }
            return test.Remove(test.Length - 1);
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
