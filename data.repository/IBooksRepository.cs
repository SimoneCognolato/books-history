using data.model.Entities;
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
        Task<Book?> GetById(int id);
        Task<bool> Add(Book book);
    }
}
