namespace ShiftType.DbModels
{
    public class Donate
    {
        public int Id { get; set; }
        public User User { get; set; }
        public double Amount { get; set; }
    }
}
