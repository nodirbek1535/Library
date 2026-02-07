//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Models.Books;

namespace Library.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Book> InsertBookAsync(Book book);
        ValueTask<Book> SelectBookByIdAsync(Guid bookId);
        IQueryable<Book> SelectAllBooks();
    }
}
