using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Models.Dto
{
    public class InsertBookRequestDto
    {
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
