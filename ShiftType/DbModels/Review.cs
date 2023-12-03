namespace ShiftType.DbModels
{
    public class Review
    {
        public int Id { get; set; }
        
        public double Rating { get; set; }
        public User Publisher { get; set; }
        public Quote Quote { get; set; }
    }
}
