using Application.Commands.UserC.Commands;
using Application.Queries.UserQ.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator,ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var token = await _mediator.Send(command);

                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginQuery query)
        {
            try
            {
                var token = await _mediator.Send(query);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
