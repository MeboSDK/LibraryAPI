using Application.Commands.AuthorC.Commands;
using Application.Queries.AuthorQ.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IMediator mediator, ILogger<AuthorController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("add-author")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorCommand command)
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

        [HttpPost("update-author")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand command)
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

        [HttpGet("authors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                GetAllAuthorsQuery query = new GetAllAuthorsQuery();
                var authors = await _mediator.Send(query);
                var json = JsonSerializer.Serialize(authors);
                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("author")]
        public async Task<IActionResult> GetAuthorById([FromQuery] GetAuthorByIdQuery query)
        {
            try
            {
                var author = await _mediator.Send(query);
                var json = JsonSerializer.Serialize(author);
                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
