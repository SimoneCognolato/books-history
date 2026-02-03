using data.migration;
using data.model.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.repository
{
    public class InMemoryBooksRepository : IBooksRepository
    {
        private readonly BooksDbContext _dbContext;

        public InMemoryBooksRepository(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Book book)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(u => u.Id == book.Id);

            if (existingBook == null)
            {
                await _dbContext.Books.AddAsync(book);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetById(long id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<BookHistory>> GetHistoryById(long id)
        {
            return await _dbContext.BooksHistory.Where(u => u.Id == id).ToListAsync();
        }

        public async Task<bool> Update(Book book)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(u => u.Id == book.Id);

            if (existingBook == null)
                return false;

            var existingBookHistory = new BookHistory
            {
                Id = existingBook.Id,
                Title = existingBook.Title,
                Description = existingBook.Description,
                PublishDate = existingBook.PublishDate,
                Authors = existingBook.Authors,
                ChangedOn = DateTime.UtcNow
            };

            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.PublishDate = book.PublishDate;
            existingBook.Authors = book.Authors;

            bool valuesChanged = _dbContext.Entry(existingBook).Properties.Any(p => p.IsModified);

            if (!valuesChanged)
                return false;

            await _dbContext.BooksHistory.AddAsync(existingBookHistory);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private static bool AreEqual<T>(T a, T b)
        {
            return JsonConvert.SerializeObject(a) == JsonConvert.SerializeObject(b);
        }
    }
}
