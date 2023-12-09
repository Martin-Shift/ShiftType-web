using Microsoft.EntityFrameworkCore;
using ShiftType.DbModels;
using ShiftType.Models;

namespace ShiftType.Services
{
    public static class ProfileInfoService
    {
        public static Profile GenerateInfo(User user, TypingDbContext context)
        {
            var prof = context.Users.Include(x => x.Results).Include(x => x.Badge).Include(x => x.Logo).Include(x=> x.CreatedQuotes).First(x=> x.Id == user.Id);
                var profile = new Profile()
            {
                Id = prof.Id,
                Name = prof.VisibleName,
                Description = prof.Description,
                ImageUrl = prof.Logo?.Path(),
                Results = prof.Results.ToList(),
                CreatedAt = prof.CreatedAt,        
                Level = prof.Level,
                Exp  = prof.Exp,
                Badge = prof.Badge,
                CreatedQuotes = prof.CreatedQuotes.ToList()
                          };
            profile.LeaderBoard15S = GetPlacement(profile, context, 15);
            profile.LeaderBoard60S = GetPlacement(profile, context, 60);
            return profile;
        }
        public static Profile GenerateInfo(User user)
        {
            var profile = new Profile()
            {
                Id = user.Id,
                Name = user.VisibleName,
                Description = user.Description,
                ImageUrl = user.Logo?.Path(),
                Results =  user.Results.ToList(),
                CreatedAt = user.CreatedAt,
                Level = user.Level,
                Exp = user.Exp
            };
            return profile;
        }
        
        public static int GetPlacement(Profile profile, TypingDbContext context, int seconds)
        {
            return context.Users
                .Include(x => x.Results)
                .ToList()
                .OrderByDescending(x => ResultProviderService.GetBestTimeResult(x.Results.ToList(),seconds).Wpm)
                .ToList()
                .FindIndex(x => x.Id == profile.Id) + 1;
        }
    }
}
