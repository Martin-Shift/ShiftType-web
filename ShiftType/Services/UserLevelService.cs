using ShiftType.DbModels;
namespace ShiftType.Services
{
    public static class UserLevelService
    {
        public static void AddExp(User user,int xp)
        {
            user.Exp += xp;
            if(user.Exp >= GetNextLevelExp(user)) 
            {
                LevelUp(user);
            }
        }
        public static int GetNextLevelExp(User user) 
        {
            return (int)(100 * ((user.Level-1) / 1.5));
        }
        public static void LevelUp(User user)
        {
            user.Exp -= GetNextLevelExp(user);
            user.Level++;
        }
    }
}
