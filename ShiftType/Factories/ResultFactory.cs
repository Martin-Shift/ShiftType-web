using ShiftType.DbModels;

namespace ShiftType.Factories
{
    public static class ResultFactory
    {

        public static List<Result> CreateSome(int count)
        {
            var list = new List<Result>();
            for (int i = 0;i<count;i++)
            {
                list.Add(CreateRandom());
            }
            return list;
        }
        public static List<Result> CreateWithUser(User user,int count)
        {
            var list = CreateSome(count);
            list.ForEach(x => x.User = user);
            return list;
        }
        public static Result CreateRandom()
        {
            Random rnd = new Random();
            var text = GetRandomText(rnd, 10);
            return new Result
            {
                Wpm = rnd.Next(0, 100), 
                TimeSpent = 15, 
                Errors = 0,
                Text = text, 
                TypedText = text
            };
        }

        private static string GetRandomText(Random rnd, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
