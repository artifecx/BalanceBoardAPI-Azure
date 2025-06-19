using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET: api/accounts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        // GET api/accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("Value cannot be null or empty.");
            }

            return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, value);
        }

        // PUT api/accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("Value cannot be null or empty.");
            }

            return Ok();
        }

        // DELETE api/accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return NoContent();
        }
    }
}
