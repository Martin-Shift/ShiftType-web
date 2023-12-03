using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftType.DbModels
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User Publisher { get; set; }
    }
}
