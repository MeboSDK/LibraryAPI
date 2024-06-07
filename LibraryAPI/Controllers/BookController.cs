﻿using Application.Commands.BookC.Commands;
using Application.Queries.BookQ.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookController> _logger;

        public BookController(IMediator mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("AddBook")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
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

        [HttpPost("UpdateBook")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
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

        [HttpGet("Books")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                GetAllBooksQuery query = new GetAllBooksQuery();
                var Books = await _mediator.Send(query);
                var json = JsonSerializer.Serialize(Books);
                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Book")]
        public async Task<IActionResult> GetBookById([FromQuery] GetBookByIdQuery query)
        {
            try
            {
                var Book = await _mediator.Send(query);
                var json = JsonSerializer.Serialize(Book);
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
