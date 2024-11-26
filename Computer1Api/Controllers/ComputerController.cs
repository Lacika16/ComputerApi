using Computer1Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Computer1Api.Controllers
{
    [Route("computer")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly ComputerContext computerContext;

        public ComputerController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<Comp>> Post(CreateCompDto createCompDto)
        {
            var comp = new Comp
            {
                Id = Guid.NewGuid(),
                Brand = createCompDto.Brand,
                Type = createCompDto.Type,
                Display = createCompDto.Display,
                Memory = createCompDto.Memory,
                OsId = createCompDto.OsId,
                CreatedTime = DateTime.Now
            };

            if (comp != null)
            {
                await computerContext.Comps.AddAsync(comp);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, comp);
            }
            return BadRequest(new { messages = "Hiba az objektum megadásnál." });
        }
        [HttpGet]
        public async Task<ActionResult<Comp>> Get()
        {
            return Ok(await computerContext.Comps.Select(x => new { x.Brand, x.Type, x.Memory }).ToListAsync());
        }

        [HttpGet("numberOfComputers")]
        public async Task<ActionResult> GetNumberOfComputers()
        {
            var comps = await computerContext.Comps.ToArrayAsync();
            return Ok(new { message = "Sikeres", count = comps.Count() });
        }

        [HttpGet("allWindowsOsComputer")]
        public async Task<ActionResult<Comp>> GetAllWindowsOsComputer()
        {
            return Ok(await computerContext.Comps.Where(x => x.Os.Name.Contains("windows")).Select(x => new { x.Os.Name }).ToListAsync());
        }

        [HttpGet("osOrderWithDescendant")]
        public async Task<ActionResult<O>> GetOsOrderDescendant()
        {
            return Ok(await computerContext.Osystem.OrderByDescending(x => x.Name).ToListAsync());
        }
    }
}