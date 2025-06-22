using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        // GET: api/transactions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        // GET api/transactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }

        // POST api/transactions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("Value cannot be null or empty.");
            }

            return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, value);
        }

        // PUT api/transactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("Value cannot be null or empty.");
            }

            return Ok();
        }

        // DELETE api/transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return NoContent();
        }
    }
}
