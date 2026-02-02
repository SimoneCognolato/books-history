using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.model.Entities
{
    public class Book
    {
        public int EntityId { get; set; }
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public required List<string> Authors { get; set; }
    }
}
