//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Brokers.Loggings;
using Library.Api.Brokers.Storages;
using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace Library.Api.Services.Foundations.Books
{
    public partial class BookService : IBookService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public BookService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Book> AddBookAsync(Book book) =>
            TryCatch(async () =>
            {
                ValidateBookOnAdd(book);

                return await this.storageBroker.InsertBookAsync(book);
            });

        public ValueTask<Book> RetrieveBookByIdAsync(Guid bookId) =>
            TryCatch(async () =>
            {
                ValidateBookId(bookId);
                Book maybeBook = await this.storageBroker.SelectBookByIdAsync(bookId);
                ValidateStorageBook(maybeBook, bookId);
                return maybeBook;
            });

        public IQueryable<Book> RetrieveAllBooks()
        {
            try
            {
                return this.storageBroker.SelectAllBooks();
            }
            catch (SqlException sqlException)
            {
                var failedBookServiceException =
                    new FailedBookServiceException(sqlException);

                throw new BookDependencyException(failedBookServiceException);
            }
            catch (Exception exception)
            {
                var failedBookServiceException =
                    new FailedBookServiceException(exception);

                throw new BookServiceException(failedBookServiceException);
            }
        }
    }
}
