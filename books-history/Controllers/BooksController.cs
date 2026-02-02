using api.model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace books_history.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<ActionResult<List<BookDTO>>> GetBooks()
        {
            return null;
        }

        [HttpGet("{id}")]
        public Task<ActionResult<BookDTO>> GetBookById()
        {
            return null;
        }

        [HttpGet("{id}/history")]
        public Task<ActionResult<BookDTO>> GetBookHistoryById()
        {
            return null;
        }

        [HttpPost]
        public Task<ActionResult<BookDTO>> CreateBook([FromBody] BookDTO book)
        {
            return null;
        }
    }
}
