using Microsoft.AspNetCore.Mvc;
using MyBGList.DTO.v1;

namespace MyBGList.Controllers.v1
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(ILogger<BoardGamesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(NoStore = true)]
        public RestDTO<BoardGame[]> Get()
        {
            return new RestDTO<BoardGame[]>()
            {
                Data = new BoardGame[] {
                    new BoardGame() {
                        Id = 1,
                        Name = "Axis & Allies",
                        Publisher = "Milton Bradley",
                        Year = 1981
                    },
                    new BoardGame() {
                        Id = 2,
                        Name = "Citadel",
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
                Links = new List<LinkDTO> {
                    new LinkDTO(
                        Url.Action(null, "BoardGames", null, Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }
    }
}
