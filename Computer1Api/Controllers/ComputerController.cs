using Computer1Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return BadRequest(new { messages = "Hiba az objektum megadásánál" });
        }
    }
}
