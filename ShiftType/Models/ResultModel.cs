namespace ShiftType.Models
{
    public class ResultModel
    {
        public string[] TypedText { get; set; }
        public string[] OriginalText { get; set; }
        public string[][] TypedSeconds { get; set; }
        public bool? IsNumbers { get; set; }
        public bool? IsSymbols { get; set; }
        public string? Language { get; set; }
        public int? TimeSpent { get; set; }
        public TestTypes Type { get; set; }
    }
}
