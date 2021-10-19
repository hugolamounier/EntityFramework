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
        private readonly DatabaseContext _databaseContext;
        public ExampleController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _databaseContext.Books.Include("Author").ToList();
        }

        [HttpPost]
        [Route("TransactionTestRollback")]
        public async Task<IActionResult> TransactionTestRollback([FromBody] Book book)
        {

            using var transaction = _databaseContext.Database.BeginTransaction();

            try
            {
                var response = _databaseContext.Add<Book>(book);
                _databaseContext.SaveChanges();

                transaction.Commit();

                return Ok(response.Entity);
            }
            catch
            {
                transaction.Rollback();
                return BadRequest("Transaction not commited");
            }
        }

        [HttpPost]
        [Route("TransactionTestCommit")]
        public async Task<IActionResult> TransactionTestCommit([FromBody] InsertBookRequestDto bookRequest)
        {

            using var transaction = _databaseContext.Database.BeginTransaction();

            try
            {
                var authorCreated = await _databaseContext.AddAsync(bookRequest.Author);
                _databaseContext.SaveChanges();

                bookRequest.Book.Author = authorCreated.Entity;

                var response = await _databaseContext.AddAsync(bookRequest.Book);
                _databaseContext.SaveChanges();

                transaction.Commit();

                return Ok(response.Entity);
            }
            catch
            {
                transaction.Rollback();
                return BadRequest("Transaction not commited");
            }
        }

        [HttpPost]
        [Route("InserAuthor")]
        public async Task<IActionResult> InsertAuthor([FromBody] Author author)
        {

            using var transaction = _databaseContext.Database.BeginTransaction();

            try
            {
                var authorCreated = await _databaseContext.AddAsync(author);
                _databaseContext.SaveChanges();

                transaction.Commit();

                return Ok(authorCreated);
            }
            catch(Exception e)
            {
                transaction.Rollback();
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}
