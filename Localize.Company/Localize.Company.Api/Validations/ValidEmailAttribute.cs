using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Localize.Company.Api.Validations
{
    public class ValidEmailAttribute : ValidationAttribute
    {
        private static readonly Regex _emailRegex = new Regex(
             @"^(?("")("".+?[^\\]"")|(([0-9a-zA-Z](([\.\-\+_][0-9a-zA-Z])|[0-9a-zA-Z])*)+))@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z]{2,})$",
             RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var email = value.ToString();
            return _emailRegex.IsMatch(email);
        }
    }
}
