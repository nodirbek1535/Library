//===============================================
//@nodirbek1535 library api program (C)
//===============================================


//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Models.Books;

namespace Library.Api.Services.Foundations.Books
{
    public interface IBookService
    {
        ValueTask<Book> AddBookAsync(Book book);
    }
}
