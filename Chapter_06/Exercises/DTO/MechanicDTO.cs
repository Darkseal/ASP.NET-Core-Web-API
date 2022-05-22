using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MyBGList.Attributes;

namespace MyBGList.DTO
{
    public class MechanicDTO
    {
        [Required]
        public int Id { get; set; }

        [LettersOnly(UseRegex = true)]
        public string? Name { get; set; }
    }
}
