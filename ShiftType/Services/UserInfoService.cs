namespace ShiftType.Services
{
    public static class UserInfoService
    {
        public static string GetFirstPartOfEmail(string email)
        {

            if (email != null)
            {
                string[] emailParts = email.Split('@');

                if (emailParts.Length > 1)
                {
                    // The first part of the email is in emailParts[0]
                    string firstPartOfEmail = emailParts[0];

                    return firstPartOfEmail;
                }
            }
            return "";
        }
    }
}
