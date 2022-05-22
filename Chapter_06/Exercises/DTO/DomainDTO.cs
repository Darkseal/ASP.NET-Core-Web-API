using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBGList.DTO
{
    public class DomainDTO : IValidatableObject
    {
        [Required]
        public int Id { get; set; }

        [RegularExpression("^[A-Za-z]+$",
            ErrorMessage =
            "Value must contain only letters(no spaces, digits, or other chars)")]
        public string? Name { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            return (Id != 3 && Name != "Wargames")
                ? new[] { new ValidationResult(
                    "Id and/or Name values must match an allowed Domain.") }
                : new ValidationResult[0];
        }
    }
}
