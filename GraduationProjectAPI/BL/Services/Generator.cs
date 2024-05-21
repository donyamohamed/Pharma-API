namespace GraduationProjectAPI.BL.Services
{
    public static class Generator
    {
        public static string GenerateCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            // Generate a random string of length 8
            string randomString = new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }
    }
}
