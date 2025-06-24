using System.Text.RegularExpressions;

namespace Mail.Engine.Service.Core.Utils
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            // Define a regex pattern for valid email addresses
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
