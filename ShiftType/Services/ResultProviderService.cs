using ShiftType.DbModels;

namespace ShiftType.Services
{
    public static class ResultProviderService
    {

        public static Result? GetBestTimeResult(List<Result> results, int timeCount)
        {

            return results
                .Where(x => x.TimeSpent == timeCount)
                .MaxBy(x => x.Wpm) ?? new Result() { Wpm = 0, TimeSpent = 0, Errors = 0, Text = "",TypedText = ""};
        }
        public static Result? GetBestWordResult(List<Result> results, int wordCount)
        {
            return results
                .Where(x => x.Text.Split(" ").Length == wordCount)
                .MaxBy(x => x.Wpm) ?? new Result() { Wpm = 0, TimeSpent = 0, Errors = 0, Text = "", TypedText = "" };
        }

        public static double AverageBy(List<Result> items, Func<Result, double> propertySelector)
        {
            if (items == null || !items.Any())
            {
                return 0;
            }

            return Math.Floor(items.Average(propertySelector));
        }
        public static double MaxBy(List<Result> items, Func<Result, double> propertySelector)
        {
            if (items == null || !items.Any())
            {
                return 0;
            }

            return items.Max(propertySelector);
        }
    }
}
