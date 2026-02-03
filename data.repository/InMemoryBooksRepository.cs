using data.migration;
using data.model.Entities;
using data.model.Enums;
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
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(u => u.Guid == book.Guid);

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

        public async Task<Book?> GetByGuid(Guid guid)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(u => u.Guid == guid);
        }

        public async Task<bool> Update(Book book)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(u => u.Guid == book.Guid);

            if (existingBook == null)
                return false;

            var changes = new List<BookHistory>();

            if (existingBook.Title != book.Title)
            {
                AddHistory(changes, existingBook.Id, nameof(existingBook.Title), existingBook.Title, book.Title);
            }

            if (existingBook.Description != book.Description)
            {
                AddHistory(changes, existingBook.Id, nameof(existingBook.Description), existingBook.Description, book.Description);
            }

            if (existingBook.PublishDate != book.PublishDate)
            {
                AddHistory(changes, existingBook.Id, nameof(existingBook.PublishDate), existingBook.PublishDate.ToString(), book.PublishDate.ToString());
            }

            if (existingBook.Authors.Count != book.Authors.Count || existingBook.Authors.Except(book.Authors).Any())
            {
                AddHistory(changes, existingBook.Id, nameof(existingBook.Authors), string.Join(", ", existingBook.Authors), string.Join(", ", book.Authors));
            }

            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.PublishDate = book.PublishDate;
            existingBook.Authors = book.Authors;

            bool valuesChanged = _dbContext.Entry(existingBook).Properties.Any(p => p.IsModified);

            if (valuesChanged)
            {
                await _dbContext.BooksHistory.AddRangeAsync(changes);
                await _dbContext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<BookHistory>> GetHistoryByGuid(Guid guid, UpdatedFieldEnum? updatedField)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(u => u.Guid == guid);

            if (existingBook == null)
                return new List<BookHistory>();

            var query = _dbContext.BooksHistory.Where(u => u.BookId == existingBook.Id);

            if (updatedField.HasValue)
            {
                query = query.Where(u => u.UpdatedField == updatedField.Value.ToString());
            }

            return await query.ToListAsync();

        }

        private void AddHistory(List<BookHistory> historyList, long id, string updatedField, string previousValue, string  currentValue)
        {
            var bookHistory = new BookHistory
            {
                BookId = id,
                UpdatedOn = DateTime.UtcNow,
                UpdatedField = updatedField,
                PreviousValue = previousValue,
                CurrentValue = currentValue
            };
            historyList.Add(bookHistory);
        }
    }
}
