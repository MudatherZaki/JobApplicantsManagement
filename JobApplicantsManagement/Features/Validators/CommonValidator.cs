using System.Text.RegularExpressions;

namespace JobApplicantsManagement.Features.Validators
{
    public static class CommonValidator
    {
        private const string PhoneNumberRegex = "^(\\+\\d{1,2}\\s?)?1?\\-?\\.?\\s?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$";

        public static bool IsPhoneNumberValid(string? phoneNumber)
        {
            if(phoneNumber == null)
            {
                return true;
            }

            var regex = new Regex(PhoneNumberRegex);
            return regex.IsMatch(phoneNumber);
        }
    }
}
