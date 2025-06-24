using System.Text.RegularExpressions;

namespace Mail.Engine.Service.Core.Utils
{
    public class PhoneNumberValidator
    {
        public static bool IsValidSouthAfricanCellNumber(string phoneNumber)
        {
            // Validates South African mobile numbers like: 0721234567 or +27721234567
            string pattern = @"^(?:\+27|0)(6\d|7[0-9])\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static bool IsValidUkMobileNumber(string phoneNumber)
        {
            // Matches UK mobile numbers like: 07123456789 or +447123456789
            string pattern = @"^(?:\+44|0)7\d{9}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static bool IsValidZimbabweMobileNumber(string phoneNumber)
        {
            // Matches Zim numbers like: 0772123456 or +263772123456
            string pattern = @"^(?:\+263|0)7\d{8}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
