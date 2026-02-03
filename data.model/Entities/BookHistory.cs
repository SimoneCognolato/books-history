using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.model.Entities
{
    [Table("BooksHistory")]
    public class BookHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key, Required]
        public long EntityId { get; set; }

        [ForeignKey("Book")]
        public long Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public required List<string> Authors { get; set; }

        [Required]
        public DateTime ChangedOn { get; set; }
    }
}
