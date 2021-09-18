using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Data.Entities
{
    public class Author : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string firstName { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(200)")]
        public string lastName { get; set; }
        public List<Book> Books { get; set; }
    }
}
