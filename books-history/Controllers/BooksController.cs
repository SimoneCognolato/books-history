using api.model.DTOs;
using api.model.Enums;
using AutoMapper;
using data.model.Entities;
using data.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

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

            return Ok(_mapper.Map<List<BookDTO>>(books));
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<BookDTO>> GetBookByGuid([FromRoute, Required] Guid guid)
        {
            var book = await _repository.GetByGuid(guid);

            if (book == null)
                return NotFound("No book found");

            return Ok(_mapper.Map<BookDTO>(book));
        }

        [HttpGet("{guid}/history")]
        public async Task<ActionResult<List<BookHistory>>> GetBookHistoryByGuid(
            [FromRoute, Required] Guid guid, 
            [FromQuery] UpdatedFieldEnum? updatedField,
            [FromQuery] OrderingDirectionEnum? ordering,
            [FromQuery] int? limit,
            [FromQuery] int? offset)
        {
            return await _repository.GetHistoryByGuid(
                guid, 
                _mapper.Map<data.model.Enums.UpdatedFieldEnum>(updatedField),
                _mapper.Map<data.model.Enums.OrderingDirectionEnum>(ordering),
                limit,
                offset);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] BookCreationRequestDTO bookCreationRequest)
        {
            Guid guid = Guid.NewGuid();
            var book = _mapper.Map<Book>(bookCreationRequest);
            book.Guid = guid;

            var isAdded = await _repository.Add(book);

            if (isAdded == false)
                return BadRequest("Book can not be created");

            var insertedBook = await _repository.GetByGuid(book.Guid);

            return Ok(_mapper.Map<BookDTO>(insertedBook));
        }

        [HttpPut("{guid}")]
        public async Task<ActionResult<BookDTO>> UpdateBook([FromRoute, Required] Guid guid, [FromBody] BookCreationRequestDTO book)
        {
            var existingBook = await _repository.GetByGuid(guid);

            if (book == null)
                return NotFound("No book found");

            var bookToInsert = _mapper.Map<Book>(book);
            bookToInsert.Guid = guid;

            var update = await _repository.Update(bookToInsert);

            if (update == false)
                return BadRequest("Unable to update book");

            return Ok(book);
        }
    }
}
