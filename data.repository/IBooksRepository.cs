using data.model.Entities;
using data.model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.repository
{
    public interface IBooksRepository
    {
        Task<List<Book>> GetAll();
        Task<Book?> GetByGuid(Guid guid);
        Task<bool> Add(Book book);
        Task<bool> Update(Book book);
        Task<List<BookHistory>> GetHistoryByGuid(Guid guid, UpdatedFieldEnum? updatedField, OrderingDirectionEnum? ordering, int? limit, int? offset);
    }
}
