using Application.Commands.BookC.Commands;
using LibraryAPI.Models.BorrowModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> TakeOutBook([FromBody] TakeOutBookModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            try
            {
                TakeOutBookCommand command = new TakeOutBookCommand(userId, model.BookId, model.Count);
                
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
