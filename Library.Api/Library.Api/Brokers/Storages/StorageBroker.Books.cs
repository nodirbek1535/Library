//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Models.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Book> Books { get; set; }

        public async ValueTask<Book> InsertBookAsync(Book book)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Book> bookEntityEntry =
                await broker.Books.AddAsync(book);

            await broker.SaveChangesAsync();

            return bookEntityEntry.Entity;
        }

        public async ValueTask<Book> SelectBookByIdAsync(Guid bookId)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Books
                .FirstOrDefaultAsync(book => book.Id == bookId);
        }

        public IQueryable<Book> SelectAllBooks() =>
            SelectAll<Book>();

        public async ValueTask<Book> UpdateBookAsync(Book book)
        {
            using var broker = new StorageBroker(this.configuration);

            broker.Books.Update(book);
            await broker.SaveChangesAsync();

            return book;
        }

        public async ValueTask<Book> DeleteBookAsync(Book book)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Book> bookEntityEntry =
                broker.Books.Remove(book);

            await broker.SaveChangesAsync();

            return book;
        }
    }
}
