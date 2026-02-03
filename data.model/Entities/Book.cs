using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace data.model.Entities
{
    [Table("Books")]
    [Index(nameof(Guid), IsUnique = true)]
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key, Required]
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly PublishDate { get; set; }
        public required List<string> Authors { get; set; }
    }
}
