using CsvHelper.Configuration.Attributes;

namespace MyBGList.Models.Csv
{
    public class BggRecord
    {
        [Name("ID")]
        public int? ID { get; set; }

        public string? Name { get; set; }

        [Name("Year Published")]
        public int? YearPublished { get; set; }

        [Name("Min Players")]
        public int? MinPlayers { get; set; }

        [Name("Max Players")]
        public int? MaxPlayers { get; set; }

        [Name("Play Time")]
        public int? PlayTime { get; set; }

        [Name("Min Age")]
        public int? MinAge { get; set; }

        [Name("Users Rated")]
        public int? UsersRated { get; set; }

        [Name("Rating Average")]
        public decimal? RatingAverage { get; set; }

        [Name("BGG Rank")]
        public int? BGGRank { get; set; }

        [Name("Complexity Average")]
        public decimal? ComplexityAverage { get; set; }

        [Name("Owned Users")]
        public int? OwnedUsers { get; set; }

        public string? Mechanics { get; set; }

        public string? Domains { get; set; }
    }
}
