//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;

namespace Library.Api.Services.Foundations.Books
{
    public partial class BookService
    {
        private void ValidateBookNotNull(Book book)
        {
            if (book is null)
            {
                throw new NullBookException();
            }
        }
    }
}
