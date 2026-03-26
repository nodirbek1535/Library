//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Books;
using Library.Api.Models.ReaderBooks;
using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Library.Api.Services.Foundations.Books;
using Library.Api.Services.Foundations.Readers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderBookController : RESTFulController
    {
        private readonly IReaderService readerService;
        private readonly IBookService bookService;

        public ReaderBookController(
            IReaderService readerService,
            IBookService bookService)
        {
            this.readerService = readerService;
            this.bookService = bookService;
        }

        [HttpGet("{readerId}")]
        public async ValueTask<ActionResult> GetBooksByReaderIdAsync(Guid readerId)
        {
            try
            {
                Reader reader = await this.readerService.RetrieveReaderByIdAsync(readerId);

                List<Book> books = this.bookService
                    .RetrieveAllBooks()
                    .Where(book => book.ReaderId == readerId)
                    .ToList();

                var readerBook = new ReaderBook
                {
                    Reader = reader,
                    Books = books
                };

                return Ok(readerBook);
            }
            catch (ReaderValidationException readerValidationException)
            {
                return BadRequest(readerValidationException.InnerException);
            }
            catch (ReaderDependencyException readerDependencyException)
            {
                return InternalServerError(readerDependencyException.InnerException);
            }
            catch (ReaderServiceException readerServiceException)
            {
                return InternalServerError(readerServiceException.InnerException);
            }
        }
    }
}