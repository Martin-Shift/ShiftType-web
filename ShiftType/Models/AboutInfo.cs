namespace ShiftType.Models
{
    public class AboutInfo
    {
        public int TotalTests { get; set; }
        public int TotalTypingTime { get; set; }
        public int TotalUsers { get; set; }
        public List<int> UserWpmChart { get; set; }
        public List<string> UserWpmChartLabels { get; set; }
    }
}
