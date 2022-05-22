using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList.DTO;
using MyBGList.Models;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.ComponentModel.DataAnnotations;
using MyBGList.Attributes;

namespace MyBGList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MechanicsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<MechanicsController> _logger;

        public MechanicsController(
            ApplicationDbContext context,
            ILogger<MechanicsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetMechanics")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<RestDTO<Mechanic[]>> Get(
            [FromQuery] RequestDTO<MechanicDTO> input)
        {
            var query = _context.Mechanics.AsQueryable();
            if (!string.IsNullOrEmpty(input.FilterQuery))
                query = query.Where(b => b.Name.Contains(input.FilterQuery));
            query = query
                    .OrderBy($"{input.SortColumn} {input.SortOrder}")
                    .Skip(input.PageIndex * input.PageSize)
                    .Take(input.PageSize);

            return new RestDTO<Mechanic[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                RecordCount = await _context.Mechanics.CountAsync(),
                Links = new List<LinkDTO> {
                    new LinkDTO(
                        Url.Action(
                            null,
                            "Mechanics",
                            new { input.PageIndex, input.PageSize },
                            Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }

        [HttpPost(Name = "UpdateMechanic")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<Mechanic?>> Post(MechanicDTO model)
        {
            var mechanic = await _context.Mechanics
                .Where(b => b.Id == model.Id)
                .FirstOrDefaultAsync();
            if (mechanic != null)
            {
                if (!string.IsNullOrEmpty(model.Name))
                    mechanic.Name = model.Name;
                mechanic.LastModifiedDate = DateTime.Now;
                _context.Mechanics.Update(mechanic);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<Mechanic?>()
            {
                Data = mechanic,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                            Url.Action(
                                null,
                                "Mechanics",
                                model,
                                Request.Scheme)!,
                            "self",
                            "POST"),
                }
            };
        }

        [HttpDelete(Name = "DeleteMechanic")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<Mechanic?>> Delete(int id)
        {
            var mechanic = await _context.Mechanics
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            if (mechanic != null)
            {
                _context.Mechanics.Remove(mechanic);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<Mechanic?>()
            {
                Data = mechanic,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                            Url.Action(
                                null,
                                "Mechanics",
                                id,
                                Request.Scheme)!,
                            "self",
                            "DELETE"),
                }
            };
        }
    }
}
