//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Brokers.Loggings;
using Library.Api.Brokers.Storages;
using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Microsoft.Identity.Client;

namespace Library.Api.Services.Foundations.Books
{
    public class BookService : IBookService
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

        public async ValueTask<Book> AddBookAsync(Book book)
        {
            try
            {
                if (book is null)
                {
                    throw new NullBookException();
                }

                return await this.storageBroker.InsertBookAsync(book);
            }
            catch (NullBookException nullBookException)
            {
                var bookValidationException =
                    new BookValidationException(nullBookException);

                this.loggingBroker.LogError(bookValidationException);

                throw bookValidationException;
            }
        }
    }
}
