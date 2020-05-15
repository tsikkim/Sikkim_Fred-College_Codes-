using System;
using System.Linq;

namespace SikkimGov.Platform.Business.Common.Utilities
{
    public static class PasswordGenerator
    {
        private const string CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static Random random = new Random();

        public static string GenerateRandomPassword(int length = 8)
        {
            return RandomString(length);
        }

        private static string RandomString(int length)
        {
            return new string(Enumerable.Repeat(CHARACTERS, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
