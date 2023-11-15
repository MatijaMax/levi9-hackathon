using FibaApi.Players.Queries;
using FibaCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FibaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlayerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Player>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPlayers()
        {
            List<Player> players = await _mediator.Send(new GetAllPlayers.Query { });
            return Ok(players);
        }


    }
}
