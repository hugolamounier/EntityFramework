using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Key, Required]
        public Author Author { get; set; }  

        public DateTime CreatedDate {  get; set; }  
    }
}
