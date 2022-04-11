using Microsoft.AspNetCore.Mvc;
using MyBGList.DTO.v2;

namespace MyBGList.Controllers.v2
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(ILogger<BoardGamesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 120)]
        public RestDTO<BoardGame[]> Get()
        {
            return new RestDTO<BoardGame[]>()
            {
                Items = new [] {
                    new BoardGame() {
                        Id = 1,
                        Name = "Axis & Allies",
                        Publisher = "Milton Bradley",
                        Year = 1981
                    },
                    new BoardGame() {
                        Id = 2,
                        Name = "Citadels",
                        Publisher = "Hans im Glück",
                        Year = 2000
                    },
                    new BoardGame() {
                        Id = 3,
                        Name = "Terraforming Mars",
                        Publisher = "FryxGames",
                        Year = 2016
                    }
                },
                Links = new List<DTO.v1.LinkDTO> {
                    new DTO.v1.LinkDTO(
                        Url.Action(null, "BoardGames", null, Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }
    }
}
