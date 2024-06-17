using System.Text.RegularExpressions;

namespace RentalSystem.Entities.Helpers
{
    public static class Validator
    {

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string universalPhoneNumberPattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, universalPhoneNumberPattern);
        }

        public static bool IsValidPrice(decimal value)
        {
            if (value <= 0)
            {
                return false;
            }

            return true;
        }

    }
}
