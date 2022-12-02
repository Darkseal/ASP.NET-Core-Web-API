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

        [HttpPost]
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

        [HttpGet("{id}")]
        public async Task<DomainResponse> GetDomain(int id)
        {
            using var channel = GrpcChannel
                .ForAddress("https://localhost:40443");
            var client = new gRPC.Grpc.GrpcClient(channel);
            var response = await client.GetDomainAsync(
                              new DomainRequest { Id = id });
            return response;
        }

        [HttpGet]
        public async Task<DomainResponse> UpdateDomain(
            string token,
            int id,
            string name)
        {
            var headers = new Metadata();
            headers.Add("Authorization", $"Bearer {token}");

            using var channel = GrpcChannel
                .ForAddress("https://localhost:40443");
            var client = new gRPC.Grpc.GrpcClient(channel);
            var response = await client.UpdateDomainAsync(
                                new UpdateDomainRequest
                                {
                                    Id = id,
                                    Name = name
                                },
                                headers);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<MechanicResponse> GetMechanic(int id)
        {
            using var channel = GrpcChannel
                .ForAddress("https://localhost:40443");
            var client = new gRPC.Grpc.GrpcClient(channel);
            var response = await client.GetMechanicAsync(
                              new MechanicRequest { Id = id });
            return response;
        }

        [HttpGet]
        public async Task<MechanicResponse> UpdateMechanic(
            string token,
            int id,
            string name)
        {
            var headers = new Metadata();
            headers.Add("Authorization", $"Bearer {token}");

            using var channel = GrpcChannel
                .ForAddress("https://localhost:40443");
            var client = new gRPC.Grpc.GrpcClient(channel);
            var response = await client.UpdateMechanicAsync(
                                new UpdateMechanicRequest
                                {
                                    Id = id,
                                    Name = name
                                },
                                headers);
            return response;
        }
    }
}
