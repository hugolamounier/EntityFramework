using EntityFramework.Adapter;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly MySQLDatabaseContext _mySqlDatabaseContext;
        public ExampleController(MySQLDatabaseContext mySqlDatabaseContext)
        {
            _mySqlDatabaseContext = mySqlDatabaseContext;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {

            return _mySqlDatabaseContext.Books.Include("Author").ToList();
        }
    }
}
