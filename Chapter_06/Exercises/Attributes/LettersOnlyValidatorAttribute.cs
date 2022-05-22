using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyBGList.Attributes
{
    public class LettersOnlyValidatorAttribute : ValidationAttribute
    {
        public bool UseRegex { get; set; } = false;

        public LettersOnlyValidatorAttribute()
            : base("Value must contain only letters (no spaces, digits, or other chars)") { }

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var strValue = value as string;
            if (!string.IsNullOrEmpty(strValue)
                && (

                // using Regular Expressions
                (UseRegex && Regex.IsMatch(strValue, "^[A-Za-z]+$"))

                // using string manipulation methods
                || strValue.All(Char.IsLetter)

                ))
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
    }
}