using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList.DTO;
using MyBGList.Models;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.ComponentModel.DataAnnotations;
using MyBGList.Attributes;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using MyBGList.Constants;

namespace MyBGList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<DomainsController> _logger;

        public DomainsController(
            ApplicationDbContext context,
            ILogger<DomainsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetDomains")]
        [ResponseCache(CacheProfileName = "Any-60")]
        [ManualValidationFilter]
        public async Task<ActionResult<RestDTO<Domain[]>>> Get(
            [FromQuery] RequestDTO<DomainDTO> input)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] =
                    Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                if (ModelState.Keys.Any(k => k == "PageSize"))
                {
                    details.Type =
                        "https://tools.ietf.org/html/rfc7231#section-6.6.2";
                    details.Status = StatusCodes.Status501NotImplemented;
                    return new ObjectResult(details) {
                        StatusCode = StatusCodes.Status501NotImplemented
                    };
                }
                else
                {
                    details.Type =
                        "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    details.Status = StatusCodes.Status400BadRequest;
                    return new BadRequestObjectResult(details);
                }
            }

            var query = _context.Domains.AsQueryable();
            if (!string.IsNullOrEmpty(input.FilterQuery))
                query = query.Where(b => b.Name.Contains(input.FilterQuery));
            query = query
                    .OrderBy($"{input.SortColumn} {input.SortOrder}")
                    .Skip(input.PageIndex * input.PageSize)
                    .Take(input.PageSize);

            return new RestDTO<Domain[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                RecordCount = await _context.Domains.CountAsync(),
                Links = new List<LinkDTO> {
                    new LinkDTO(
                        Url.Action(
                            null,
                            "Domains",
                            new { input.PageIndex, input.PageSize },
                            Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }

        [Authorize(Roles = RoleNames.Moderator)]
        [HttpPost(Name = "UpdateDomain")]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<RestDTO<Domain?>> Post(DomainDTO model)
        {
            var domain = await _context.Domains
                .Where(b => b.Id == model.Id)
                .FirstOrDefaultAsync();
            if (domain != null)
            {
                if (!string.IsNullOrEmpty(model.Name))
                    domain.Name = model.Name;
                domain.LastModifiedDate = DateTime.Now;
                _context.Domains.Update(domain);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<Domain?>()
            {
                Data = domain,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                            Url.Action(
                                null,
                                "Domains",
                                model,
                                Request.Scheme)!,
                            "self",
                            "POST"),
                }
            };
        }

        [Authorize(Roles = RoleNames.Administrator)]
        [HttpDelete(Name = "DeleteDomain")]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<RestDTO<Domain?>> Delete(int id)
        {
            var domain = await _context.Domains
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            if (domain != null)
            {
                _context.Domains.Remove(domain);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<Domain?>()
            {
                Data = domain,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                            Url.Action(
                                null,
                                "Domains",
                                id,
                                Request.Scheme)!,
                            "self",
                            "DELETE"),
                }
            };
        }
    }
}
