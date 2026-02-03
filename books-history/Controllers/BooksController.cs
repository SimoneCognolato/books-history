using api.model.DTOs;
using AutoMapper;
using data.model.Entities;
using data.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace books_history.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBooksRepository _repository;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IBooksRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
        {
            var books = await _repository.GetAll();

            if (!books.Any())
                return NotFound("No books found");

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBookById([FromRoute, Required] long id)
        {
            var book = await _repository.GetById(id);

            if (book == null)
                return NotFound("No book found");

            return Ok(book);
        }

        [HttpGet("{id}/history")]
        public async Task<ActionResult<List<BookHistoryDTO>>> GetBookHistoryById([FromRoute, Required] long id)
        {
            var history = await _repository.GetHistoryById(id);

            if (history == null)
                return NotFound("No history was found for this book");

            var toReturn = new List<BookHistoryDTO>();

            foreach (var item in history)
            {
                var mapped = _mapper.Map<BookHistoryDTO>(item);
                toReturn.Add(mapped);
            }

            return toReturn;
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] BookCreationRequestDTO book)
        {
            var isAdded = await _repository.Add(_mapper.Map<Book>(book));

            if (isAdded == false)
                return BadRequest("Book can not be created");

            return Ok("Book successfully created");

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> UpdateBook([FromRoute, Required] long id, [FromBody] BookCreationRequestDTO book)
        {
            var existingBook = await _repository.GetById(id);

            if (book == null)
                return NotFound("No book found");

            var bookToInsert = _mapper.Map<Book>(book);
            bookToInsert.Id = id;

            var update = await _repository.Update(bookToInsert);

            if (update == false) 
                return BadRequest("Unable to update book");

            return Ok(book);
        }
    }
}
