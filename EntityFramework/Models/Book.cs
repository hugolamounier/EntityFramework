using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EntityFramework.Models
{
    [Table("book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        [JsonIgnore]
        public virtual Author Author { get; set; }  

        public DateTime CreatedDate {  get; set; }  
    }
}
