using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.model.DTOs
{
    public class BookDTO
    {
        public int id {  get; set; }
        public required string title {  get; set; }
        public required string description { get; set; }
        public DateTime publishDate { get; set; }
        public required List<string> authors { get; set; }
    }
}
