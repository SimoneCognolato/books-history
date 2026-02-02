using data.migration;
using data.model.Entities;
using Microsoft.EntityFrameworkCore;
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

            if (existingBook != null)
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

        public async Task<Book?> GetById(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
