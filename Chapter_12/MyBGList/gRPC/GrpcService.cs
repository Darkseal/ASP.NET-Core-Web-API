using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MyBGList.Constants;
using MyBGList.Models;

namespace MyBGList.gRPC
{
    public class GrpcService : Grpc.GrpcBase
    {
        private readonly ApplicationDbContext _context;

        public GrpcService(ApplicationDbContext context)
        {
            _context = context;
        }

        public override async Task<BoardGameResponse> GetBoardGame(
            BoardGameRequest request, 
            ServerCallContext scc)
        {
            var bg = await _context.BoardGames
                .Where(bg => bg.Id == request.Id)
                .FirstOrDefaultAsync();
            var response = new BoardGameResponse();
            if (bg != null)
            {
                response.Id = bg.Id;
                response.Name = bg.Name;
                response.Year = bg.Year;
            }
            return response;
        }

        [Authorize(Roles = RoleNames.Moderator)]
        public override async Task<BoardGameResponse> UpdateBoardGame(
            UpdateBoardGameRequest request,
            ServerCallContext scc)
        {
            var bg = await _context.BoardGames
                .Where(bg => bg.Id == request.Id)
                .FirstOrDefaultAsync();
            var response = new BoardGameResponse();
            if (bg != null)
            {
                bg.Name = request.Name;
                _context.BoardGames.Update(bg);
                await _context.SaveChangesAsync();
                response.Id = bg.Id;
                response.Name = bg.Name;
                response.Year = bg.Year;
            }
            return response;
        }
    }
}
