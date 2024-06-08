using Application.Commands.BookC.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookBorrowController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookBorrowController> _logger;

        public BookBorrowController(IMediator mediator, ILogger<BookBorrowController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("takeoutbook")]
        [Authorize]
        public async Task<IActionResult> TakeOutBook([FromBody] TakeOutBookCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("returnbook")]
        [Authorize]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
