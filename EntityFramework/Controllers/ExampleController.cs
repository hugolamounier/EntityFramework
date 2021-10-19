using EntityFramework.Adapter;
using EntityFramework.Models;
using EntityFramework.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly DatabaseContext _mySqlDatabaseContext;
        public ExampleController(DatabaseContext mySqlDatabaseContext)
        {
            _mySqlDatabaseContext = mySqlDatabaseContext;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _mySqlDatabaseContext.Books.Include("Author").ToList();
        }

        [HttpPost]
        [Route("TransactionTestRollback")]
        public async Task<IActionResult> TransactionTestRollback([FromBody] Book book)
        {

            using (var transaction = _mySqlDatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    var response = await _mySqlDatabaseContext.AddAsync<Book>(book);
                    transaction.Commit();

                    return Ok(response);
                }
                catch
                {
                    transaction.Rollback();
                    return BadRequest("Transaction not commited");
                }
            }
        }

        [HttpPost]
        [Route("TransactionTestCommit")]
        public async Task<IActionResult> TransactionTestCommit([FromBody] InsertBookRequestDto bookRequest)
        {

            using (var transaction = _mySqlDatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    var authorCreated = await _mySqlDatabaseContext.AddAsync(bookRequest.Author);

                    bookRequest.Book.Author = authorCreated.Entity;

                    var response = await _mySqlDatabaseContext.AddAsync(bookRequest.Book);
                    transaction.Commit();

                    return Ok(response);
                }
                catch
                {
                    transaction.Rollback();
                    return BadRequest("Transaction not commited");
                }
            }
        }
    }
}
