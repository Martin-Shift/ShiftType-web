using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftType.DbModels
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            Results = new HashSet<Result>();
            CreatedQuotes = new HashSet<Quote>();
        }
        public int Level { get; set; }
        public int Exp { get; set; }
        public virtual ImageFile? Logo { get; set; }
        public string Description { get; set; }
        public string VisibleName { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        [InverseProperty(nameof(Quote.Publisher))]
        public virtual ICollection<Quote> CreatedQuotes  { get; set; }
        public DateTime CreatedAt { get; set; }
        public Badge? Badge { get; set; }
    }
}
