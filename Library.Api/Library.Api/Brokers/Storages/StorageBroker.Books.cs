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
            EntityEntry<Book> bookEntityEntry =
                await this.Books.AddAsync(book);

            await this.SaveChangesAsync();

            return bookEntityEntry.Entity;
        }

        public async ValueTask<Book> SelectBookByIdAsync(Guid bookId) =>
            await this.Books
                .FirstOrDefaultAsync(book => book.Id == bookId);

        public IQueryable<Book> SelectAllBooks() =>
            SelectAll<Book>();

        public async ValueTask<Book> UpdateBookAsync(Book book)
        {
            this.Books.Update(book);

            await this.SaveChangesAsync();

            return book;
        }

        public async ValueTask<Book> DeleteBookAsync(Book book)
        {
            EntityEntry<Book> bookEntityEntry =
                this.Books.Remove(book);

            await this.SaveChangesAsync();

            return book;
        }
    }
}
