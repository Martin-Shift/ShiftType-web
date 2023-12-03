namespace ShiftType.Models
{
    public enum TestTypes
    {
        Time,Words,Quote,Custom
    }
    public enum QuoteType
    {
        All,Short,Medium,Large,Thicc
    }
    public class Modifiers
    {
        public TestTypes? TestType { get; set; }
        public int? TimeAmount { get; set; }
        public int? WordCount { get; set; }
        public string? CustomText { get; set; }
        public bool? IsNumbers { get; set; }
        public bool? IsSymbols {get; set; }
        public string? Language { get; set; }
        public QuoteType? QuoteType { get; set; }
    }
}
