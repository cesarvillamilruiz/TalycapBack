using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Data.Entities
{
    public class Book : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string title { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(200)")]
        public string description { get; set; }
        [Required]
        public int pageCount { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(200)")]
        public string excerpt { get; set; }
        [Required]
        public DateTime publishDate { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
