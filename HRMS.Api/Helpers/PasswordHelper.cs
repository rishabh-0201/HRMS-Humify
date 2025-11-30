using System.Security.Cryptography;
using System.Text;

namespace HRMS.Api.Helpers
{
    public class PasswordHelper
    {
        // Hash the password using SHA256
        public static string HashPassword(string password)
        {

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // compare user input with stored hash
        public static bool VerifyPassword(string password, string storedHash)
        {
            string enteredHash = HashPassword(password);
            return enteredHash == storedHash;
        }
    }
}
