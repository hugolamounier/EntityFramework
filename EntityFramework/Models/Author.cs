using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{
    public class Author
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        public string Name { get; set; }
    }
}
