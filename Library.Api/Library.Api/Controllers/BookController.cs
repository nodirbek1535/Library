//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Library.Api.Services.Foundations.Books;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : RESTFulController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService) =>
            this.bookService = bookService;

        [HttpPost]
        public async ValueTask<ActionResult> PostBookAsync(Book book)
        {
            try
            {
                Book addedBook = await this.bookService.AddBookAsync(book);

                return Created(addedBook);
            }
            catch (BookValidationException bookValidationException)
            {
                return BadRequest(bookValidationException.InnerException);
            }
            catch (BookDependencyValidationException bookDependencyValidationException)
                when (bookDependencyValidationException.InnerException is AlreadyExistBookException)
            {
                return Conflict(bookDependencyValidationException.InnerException);
            }
            catch (BookDependencyException bookDependencyException)
            {
                return InternalServerError(bookDependencyException.InnerException);
            }
            catch (BookServiceException bookServiceException)
            {
                return InternalServerError(bookServiceException.InnerException);
            }
        }

        [HttpGet("{bookId}")]
        public async ValueTask<ActionResult> GetBookByIdAsync(Guid bookId)
        {
            try
            {
                Book retrievedBook =
                    await this.bookService.RetrieveBookByIdAsync(bookId);

                return Ok(retrievedBook);
            }
            catch (BookValidationException bookValidationException)
            {
                return BadRequest(bookValidationException.InnerException);
            }
            catch (BookDependencyException bookDependencyException)
            {
                return InternalServerError(bookDependencyException.InnerException);
            }
            catch (BookServiceException bookServiceException)
            {
                return InternalServerError(bookServiceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Book>> GetAllBooks()
        {
            try
            {
                IQueryable<Book> allBooks = 
                    this.bookService.RetrieveAllBooks();

                return Ok(allBooks);
            }
            catch (BookDependencyException bookDependencyException)
            {
                return InternalServerError(bookDependencyException.InnerException);
            }
            catch (BookServiceException bookServiceException)
            {
                return InternalServerError(bookServiceException.InnerException);
            }
        }
    }
}
