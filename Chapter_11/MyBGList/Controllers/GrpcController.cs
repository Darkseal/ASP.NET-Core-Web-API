using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using MyBGList.gRPC;
using Grpc.Core;

namespace MyBGList.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class GrpcController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<BoardGameResponse> GetBoardGame(int id)
        {
            using var channel = GrpcChannel
                .ForAddress("https://localhost:40443");
            var client = new gRPC.Grpc.GrpcClient(channel);
            var response = await client.GetBoardGameAsync(
                              new BoardGameRequest { Id = id });
            return response;
        }

        [HttpGet]
        public async Task<BoardGameResponse> UpdateBoardGame(
            string token,
            int id,
            string name)
        {
            var headers = new Metadata();
            headers.Add("Authorization", $"Bearer {token}");

            using var channel = GrpcChannel
                .ForAddress("https://localhost:40443");
            var client = new gRPC.Grpc.GrpcClient(channel);
            var response = await client.UpdateBoardGameAsync(
                                new UpdateBoardGameRequest
                                {
                                    Id = id,
                                    Name = name
                                },
                                headers);
            return response;
        }
    }
}
