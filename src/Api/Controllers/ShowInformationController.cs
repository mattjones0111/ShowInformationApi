using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ShowInformationController : Controller
    {
        private readonly IShowInformationRepository _provider;

        public ShowInformationController(IShowInformationRepository provider)
        {
            _provider = provider;
        }

        [HttpGet("shows")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 ||
                pageSize < 1 ||
                pageSize > Constants.MaximumPageSize)
            {
                return BadRequest();
            }

            IEnumerable<Show> result = await _provider.GetShowsAsync(
                pageNumber,
                pageSize);

            return Ok(result);
        }

        [HttpPut("shows/{id:int}")]
        public async Task<IActionResult> Put([FromBody] Show show)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _provider.PutAsync(show);

            return NoContent();
        }
    }
}
