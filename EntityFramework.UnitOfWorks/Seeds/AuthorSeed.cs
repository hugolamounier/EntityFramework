using System;
using System.Collections;
using System.Collections.Generic;
using EntityFramework.Models;

namespace EntityFramework.UnitOfWorks.Seeds
{
    public static class AuthorSeed
    {
        public static IEnumerable<Author> Data =>
            new List<Author>()
            {
                new() { Id = 1, Name = "Nome Teste", Birthday = new DateTime(2021, 10, 19, 0, 0, 0)},
            };
    }
}