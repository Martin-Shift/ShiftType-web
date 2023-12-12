using ShiftType.DbModels;
using ShiftType.Services;
using static System.Net.Mime.MediaTypeNames;

namespace ShiftType.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Level { get; set; }

        public int Exp { get; set; }

        public List<Result> Results { get; set; }

        public int TestsCompleted { get => Results.Count; }

        public int LeaderBoard15S { get; set; }

        public int LeaderBoard60S { get; set; }

        public Badge? Badge { get; set; }

        public TimeSpan TotalTimeTyped { get => TimeSpan.FromSeconds(Results.Sum(x => x.TimeSpent)); }

        public Result? BestResult { get => Results.MaxBy(x => x.Wpm) ?? new Result() { Wpm = 0, TimeSpent = 0, Errors = 0, Text = "", TypedText = "" }; }

        public Result? Best15SResult { get => ResultProviderService.GetBestTimeResult(Results, 15); }

        public Result? Best30SResult { get => ResultProviderService.GetBestTimeResult(Results, 30); }

        public Result? Best60SResult { get => ResultProviderService.GetBestTimeResult(Results, 60); }

        public Result? Best120SResult { get => ResultProviderService.GetBestTimeResult(Results, 120); }

        public Result? Best10WordsResult { get => ResultProviderService.GetBestWordResult(Results, 10); }

        public Result? Best25WordsResult { get => ResultProviderService.GetBestWordResult(Results, 25); }

        public Result? Best50WordsResult { get => ResultProviderService.GetBestWordResult(Results, 50); }

        public Result? Best100WordsResult { get => ResultProviderService.GetBestWordResult(Results, 100); }

        public List<Quote> CreatedQuotes { get; set; }

        public int WordsTyped { get => Results.Sum(x => x.TypedText.Split(" ").Length); }

        public List<double> WpmChart { get => Results.Select(x => x.Wpm).ToList(); }

        public List<int> TimeTypedChart { get => 
            Results
            .GroupBy(x => x.Date.Date)
            .Select(group => group.Select(item => item.TimeSpent).Aggregate(0, (total, next) => total + (int)next))
            .ToList();
        }
        public List<string> TimeTypedChartLabels { get => Results.Select(x => x.Date.Date.ToString("d")).Distinct().ToList(); }

        public List<int> TestBracketsChart
        {
            get => Results.GroupBy(x => ((int)x.Wpm / 10) * 10)
                .OrderBy(x => x.Key)
                .Select(x => x.Count())
                .ToList();
        }
        public List<string> TestBracketsChartLabels
        {
            get => Results.GroupBy(x => ((int)x.Wpm / 10) * 10)
                .OrderBy(x => x.Key)
                .Select(g=> g.Key)
            .Select(i => $"{i}-{i + 9}")
            .ToList();
        }
        public List<Donate> Donates { get; set; }
        public Profile() { }


    }
}

