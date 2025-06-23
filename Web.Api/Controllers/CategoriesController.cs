using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("Value cannot be null or empty.");
            }

            return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("Value cannot be null or empty.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return NoContent();
        }
    }
}
