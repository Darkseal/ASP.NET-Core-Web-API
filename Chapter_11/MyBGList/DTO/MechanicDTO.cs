using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBGList.DTO
{
    public class MechanicDTO
    {
        [Required]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
