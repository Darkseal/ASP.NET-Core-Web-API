﻿using Microsoft.AspNetCore.Mvc;

namespace MyBGList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(ILogger<BoardGamesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBoardGames")]
        public IEnumerable<BoardGame> Get()
        {
            return new[] {
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
            };
        }
    }
}
