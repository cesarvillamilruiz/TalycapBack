using System.ComponentModel.DataAnnotations;

namespace Logic_Layer.DTO
{
    public class AuthorDTO
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
    }
}
