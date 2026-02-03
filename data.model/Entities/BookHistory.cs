using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.model.Entities
{
    [Table("BooksHistory")]
    public class BookHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key, Required]
        public long Id { get; set; }

        [ForeignKey("Book"), Required]
        public long BookId { get; set; }

        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedField { get; set; }
        public required string PreviousValue { get; set; }
        public required string CurrentValue { get; set; }
    }
}
