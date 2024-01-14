using web_store_server.Domain.Entities;

namespace web_store_server.Common.Extensions
{
    public static class UserExtensions
    {
        public static void EncryptPassword(this User user, string password)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassoword(this User user, string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, user.Password);
        }
    }
}
