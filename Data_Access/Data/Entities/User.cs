using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Data.Entities
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string userName { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string password { get; set; }
    }
}
