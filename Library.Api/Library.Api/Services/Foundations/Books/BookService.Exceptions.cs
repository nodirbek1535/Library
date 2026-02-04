//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Xeptions;

namespace Library.Api.Services.Foundations.Books
{
    public partial class BookService
    {
        private delegate ValueTask<Book> ReturningBookFunction();

        private async ValueTask<Book> TryCatch(ReturningBookFunction returningBookFunction)
        {
            try
            {
                return await returningBookFunction();
            }
            catch(NullBookException nullBookException)
            {
                throw CreateAndLogValidationException(nullBookException);
            }
        }

        private BookValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var bookValidationException =
                new BookValidationException(exception);

            this.loggingBroker.LogError(bookValidationException);

            return bookValidationException;
        }
    }
}
