using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        public virtual Author Author { get; set; }  

        public DateTime CreatedDate {  get; set; }  
    }
}
