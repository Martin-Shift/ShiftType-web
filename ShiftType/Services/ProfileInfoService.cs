using ShiftType.DbModels;
using ShiftType.Models;

namespace ShiftType.Services
{
    public static class ProfileInfoService
    {
        public static Profile GenerateInfo(User user, TypingDbContext context)
        {
            var profile = new Profile()
            {
                Id = user.Id,
                Name = user.VisibleName,
                Description = user.Description,
                ImageUrl = user.Logo.Path(),
            };
            return profile;
        }
    }
}
