using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBGList.Model
{
    public class BoardGames_Categories
    {
        [Key]
        [Required]
        public int BoardGameId { get; set; }

        [Key]
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public BoardGame? BoardGame { get; set; }

        public Category? Category { get; set; }

    }
}
