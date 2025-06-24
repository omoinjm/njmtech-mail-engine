using System.Text.RegularExpressions;

namespace Mail.Engine.Service.Core.Utils
{
    public static class PhoneNumberValidator
    {
        private static readonly Regex SaRegex = new(@"^(?:\+?27|0)(6\d|7\d)\d{7}$", RegexOptions.Compiled);
        private static readonly Regex UkRegex = new(@"^(?:\+?44|0)7\d{9}$", RegexOptions.Compiled);
        private static readonly Regex ZwRegex = new(@"^(?:\+?263|0)7\d{8}$", RegexOptions.Compiled);

        public static bool IsValidSouthAfricanCellNumber(string phoneNumber)
            => SaRegex.IsMatch(phoneNumber.Trim());

        public static bool IsValidUkMobileNumber(string phoneNumber)
            => UkRegex.IsMatch(phoneNumber.Trim());

        public static bool IsValidZimbabweMobileNumber(string phoneNumber)
            => ZwRegex.IsMatch(phoneNumber.Trim());

        public static bool IsValidPhoneNumber(string phoneNumber, string alpha3CountryCode)
        {
            phoneNumber = phoneNumber.Trim();

            return alpha3CountryCode switch
            {
                "ZAF" => IsValidSouthAfricanCellNumber(phoneNumber),
                "GBR" => IsValidUkMobileNumber(phoneNumber),
                "ZWE" => IsValidZimbabweMobileNumber(phoneNumber),
                _ => false
            };
        }
    }
}
