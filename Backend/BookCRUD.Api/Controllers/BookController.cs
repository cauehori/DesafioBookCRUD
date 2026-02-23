using BookCRUD.Api.Application.Services;
using BookCRUD.Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookCRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookServices bookServices) : ControllerBase
    {
        private readonly IBookServices _bookServices = bookServices;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _bookServices.GetAllBooks();
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookCreateDTO bookDto)
        {
            await _bookServices.AddBook(bookDto);
            return Created("", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookUpdateDTO bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }
            await _bookServices.UpdateBook(bookDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookServices.DeleteBook(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }
    }
}
