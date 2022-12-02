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

        //[HttpGet(Name = "GetBoardGames")]
        //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        //public RestDTO<BoardGame[]> Get()
        //{
        //    var query =  _context.BoardGames;

        //    return new RestDTO<BoardGame[]>()
        //    {
        //        Data = query.ToArray(),
        //        Links = new List<LinkDTO> {
        //            new LinkDTO(
        //                Url.Action(null, "BoardGames", null, Request.Scheme)!,
        //                "self",
        //                "GET"),
        //        }
        //    };
        //}

        //[HttpGet(Name = "GetBoardGames")]
        //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        //public async Task<RestDTO<BoardGame[]>> Get()
        //{
        //    var query = _context.BoardGames;

        //    return new RestDTO<BoardGame[]>()
        //    {
        //        Data = await query.ToArrayAsync(),
        //        Links = new List<LinkDTO> {
        //            new LinkDTO(
        //                Url.Action(null, "BoardGames", null, Request.Scheme)!,
        //                "self",
        //                "GET"),
        //        }
        //    };
        //}

        //[HttpGet(Name = "GetBoardGames")]
        //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        //public async Task<RestDTO<BoardGame[]>> Get(
        //    int pageIndex = 0,
        //    int pageSize = 10)
        //{
        //    var query = _context.BoardGames
        //                .Skip(pageIndex * pageSize)
        //                .Take(pageSize);

        //    return new RestDTO<BoardGame[]>()
        //    {
        //        Data = await query.ToArrayAsync(),
        //        PageIndex = pageIndex,
        //        PageSize = pageSize,
        //        RecordCount = await _context.BoardGames.CountAsync(),
        //        Links = new List<LinkDTO> {
        //            new LinkDTO(
        //                Url.Action(
        //                    null,
        //                    "BoardGames",
        //                    new { pageIndex, pageSize },
        //                    Request.Scheme)!,
        //                "self",
        //                "GET"),
        //        }
        //    };
        //}

        //[HttpGet(Name = "GetBoardGames")]
        //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        //public async Task<RestDTO<BoardGame[]>> Get(
        //    int pageIndex = 0,
        //    int pageSize = 10,
        //    string sortColumn = "Name",
        //    string sortOrder = "ASC")
        //{
        //    var query = _context.BoardGames
        //                .OrderBy($"{sortColumn} {sortOrder}")
        //                .Skip(pageIndex * pageSize)
        //                .Take(pageSize);

        //    return new RestDTO<BoardGame[]>()
        //    {
        //        Data = await query.ToArrayAsync(),
        //        PageIndex = pageIndex,
        //        PageSize = pageSize,
        //        RecordCount = await _context.BoardGames.CountAsync(),
        //        Links = new List<LinkDTO> {
        //            new LinkDTO(
        //                Url.Action(
        //                    null,
        //                    "BoardGames",
        //                    new { pageIndex, pageSize },
        //                    Request.Scheme)!,
        //                "self",
        //                "GET"),
        //        }
        //    };
        //}

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
                query = query.Where(b => b.Name.Contains(filterQuery));
            var recordCount = await query.CountAsync();
            query = query
                    .OrderBy($"{sortColumn} {sortOrder}")
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);

            return new RestDTO<BoardGame[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = recordCount,
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
        public async Task<RestDTO<BoardGame?>> Post(BoardGameDTO model)
        {
            var boardgame = await _context.BoardGames
                .Where(b => b.Id == model.Id)
                .FirstOrDefaultAsync();
            if (boardgame != null)
            {
                if (!string.IsNullOrEmpty(model.Name))
                    boardgame.Name = model.Name;
                if (model.Year.HasValue && model.Year.Value > 0)
                    boardgame.Year = model.Year.Value;
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
                                model,
                                Request.Scheme)!,
                            "self",
                            "POST"),
                }
            };
        }

        [HttpDelete(Name = "DeleteBoardGame")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<BoardGame?>> Delete(int id)
        {
            var boardgame = await _context.BoardGames
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            if (boardgame != null)
            {
                _context.BoardGames.Remove(boardgame);
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
                                id,
                                Request.Scheme)!,
                            "self",
                            "DELETE"),
                }
            };
        }
    }
}
