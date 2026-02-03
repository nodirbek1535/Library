//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Brokers.Storages;
using Library.Api.Models.Books;

namespace Library.Api.Services.Foundations.Books
{
    public class BookService : IBookService
    {
        private readonly IStorageBroker storageBroker;

        public BookService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Book> AddBookAsync(Book book) =>
            await this.storageBroker.InsertBookAsync(book);

    }
}
