using ShiftType.DbModels;
namespace ShiftType.Services
{
    public static class UserLevelService
    {
        public static void AddExp(User user,int xp)
        {
            user.Exp += xp;
            if(user.Exp >= GetNextLevelExp(user.Level)) 
            {
                LevelUp(user);
            }
        }
        public static int GetNextLevelExp(int level) 
        {
            return (int)(100 * ((Math.Abs(level-1)) / 1.5));
        }
        public static void LevelUp(User user)
        {
            user.Exp -= GetNextLevelExp(user.Level);
            user.Level++;
        }
    }
}
