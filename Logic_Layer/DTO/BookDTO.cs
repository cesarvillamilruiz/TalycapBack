using System;
using System.ComponentModel.DataAnnotations;

namespace Logic_Layer.DTO
{
    public class BookDTO
    {
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public int pageCount { get; set; }
        [Required]
        public string excerpt { get; set; }
        [Required]
        public DateTime publishDate { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
