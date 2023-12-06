namespace ShiftType.Models
{
    public class ProfileEditModel
    {
        public string Name { get;set; }
        public string Description { get;set; }
        public IFormFile? Image { get; set; }
        public int BadgeId { get; set; }
    }
}
