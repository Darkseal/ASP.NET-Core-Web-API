using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList.DTO;
using MyBGList.Models;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace MyBGList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(
            ApplicationDbContext context,
            ILogger<BoardGamesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<RestDTO<BoardGame[]>> Get(
            int pageIndex = 0,
            int pageSize = 10,
            string? sortColumn = "Name",
            string? sortOrder = "ASC",
            string? filterQuery = null)
        {
            var query = _context.BoardGames.AsQueryable();
            if (!string.IsNullOrEmpty(filterQuery))
                query = query.Where(b => b.Name.StartsWith(filterQuery));
            query = query
                    .OrderBy($"{sortColumn} {sortOrder}")
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);

            return new RestDTO<BoardGame[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = await _context.BoardGames.CountAsync(),
                Links = new List<LinkDTO> {
                    new LinkDTO(
                        Url.Action(
                            null,
                            "BoardGames",
                            new { pageIndex, pageSize },
                            Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }

        [HttpPost(Name = "UpdateBoardGame")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<BoardGame?>> Post(BoardGameDTO bgDTO)
        {
            var boardgame = await _context.BoardGames
                .Where(b => b.Id == bgDTO.Id)
                .FirstOrDefaultAsync();
            if (boardgame != null)
            {
                if (!string.IsNullOrEmpty(bgDTO.Name))
                    boardgame.Name = bgDTO.Name;
                if (bgDTO.Year.HasValue && bgDTO.Year.Value > 0)
                    boardgame.Year = bgDTO.Year.Value;
                if (bgDTO.MinPlayers.HasValue && bgDTO.MinPlayers.Value > 0)
                    boardgame.MinPlayers = bgDTO.MinPlayers.Value;
                if (bgDTO.MaxPlayers.HasValue && bgDTO.MaxPlayers.Value > 0)
                    boardgame.MaxPlayers = bgDTO.MaxPlayers.Value;
                if (bgDTO.PlayTime.HasValue && bgDTO.PlayTime.Value > 0)
                    boardgame.PlayTime = bgDTO.PlayTime.Value;
                if (bgDTO.MinAge.HasValue && bgDTO.MinAge.Value > 0)
                    boardgame.MinAge = bgDTO.MinAge.Value;
                boardgame.LastModifiedDate = DateTime.Now;
                _context.BoardGames.Update(boardgame);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<BoardGame?>()
            {
                Data = boardgame,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                            Url.Action(
                                null,
                                "BoardGames",
                                bgDTO,
                                Request.Scheme)!,
                            "self",
                            "POST"),
                }
            };
        }

        [HttpDelete(Name = "DeleteBoardGame")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<BoardGame[]?>> Delete(string ids)
        {
            var idArray = ids.Split(',').Select(x => int.Parse(x));
            var deletedBGList = new List<BoardGame>();

            foreach (int id in idArray)
            {
                var boardgame = await _context.BoardGames
                    .Where(b => b.Id == id)
                    .FirstOrDefaultAsync();
                if (boardgame != null)
                {
                    deletedBGList.Add(boardgame);
                    _context.BoardGames.Remove(boardgame);
                    await _context.SaveChangesAsync();
                };
            }

            return new RestDTO<BoardGame[]?>()
            {
                Data = deletedBGList.Count > 0 ? deletedBGList.ToArray() : null,
                Links = new List<LinkDTO>
                    {
                        new LinkDTO(
                                Url.Action(
                                    null,
                                    "BoardGames",
                                    ids,
                                    Request.Scheme)!,
                                "self",
                                "DELETE"),
                    }
            };
        }
    }
}
