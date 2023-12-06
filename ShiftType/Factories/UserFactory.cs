using ShiftType.DbModels;

namespace ShiftType.Factories
{
    public class UserFactory
    {
        public static List<User> CreateSome(int count)
        {
            var list = new List<User>();
            for (int i = 0; i < count; i++)
            {
                list.Add(CreateRandom());
            }
            return list;
        }

        public static User CreateRandom()
        {
            Random rnd = new Random();
            var visibleName = GetRandomText(rnd, 10);
            var login = GetRandomText(rnd, 8);
            var email = $"{GetRandomText(rnd, 5)}@example.com";
            var description = GetRandomText(rnd, 20);
            var exp = 10;
            var level = 3;
            var password = GetRandomText(rnd, 12);

            var user = new User
            {
                VisibleName = visibleName,
                UserName = login,
                Email = email,
                Description = description,
                Exp = exp,
                Level = level,
                
            };
            user.Results = ResultFactory.CreateWithUser(user, 5);
            return user;
        }

        private static string GetRandomText(Random rnd, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
