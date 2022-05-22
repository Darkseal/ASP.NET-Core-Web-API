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
                Data = new [] {
                    new BoardGame() {
                        Id = 1,
                        Name = "Axis & Allies",
                        Year = 1981
                    },
                    new BoardGame() {
                        Id = 2,
                        Name = "Citadels",
                        Year = 2000
                    },
                    new BoardGame() {
                        Id = 3,
                        Name = "Terraforming Mars",
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
