using Computer1Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Computer1Api.Controllers
{
    [Route("osystem")]
    [ApiController]
    public class OsController : ControllerBase
    {
        private readonly ComputerContext computerContext;

        public OsController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<O>> Post(CreateOsDto createOsDto)
        {
            var os = new O
            {
                Id = Guid.NewGuid(),
                Name = createOsDto.Name,
                CreatedTime = DateTime.Now
            };
            if (os != null) 
            { 
                await computerContext.Os.AddAsync(os);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, os);
            
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<ActionResult<O>> Get()
        {
            return Ok( await computerContext.Os.ToListAsync());
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<O>> GetById(Guid id)
        {
            var os = await computerContext.Os.FirstOrDefaultAsync(o => o.Id == id);
            if (os != null)
            {
                return Ok(os);
            }
            return BadRequest( new {message = "Nincs találat"});
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<O>> Put(UpdateOsDto updateOsDto, Guid id)
        {
            var existingOs = await computerContext.Os.FirstOrDefaultAsync(O => O.Id == id);
            if (existingOs != null)
            {
                existingOs.Name = updateOsDto.Name;
                computerContext.Os.Update(existingOs);
                await computerContext.SaveChangesAsync();
                return Ok(existingOs);
            }
            return BadRequest(new { message = "Nincs találat" });
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<O>>Delete(Guid id)
        {
            var os = await computerContext.Os.FirstOrDefaultAsync(os => os.Id == id);
            if (os != null)
            {
                computerContext.Os.Remove(os);
                await computerContext.SaveChangesAsync();
                return Ok(new {message = "Nincs találat"});
            }
            return NotFound();
        }


    }
}
