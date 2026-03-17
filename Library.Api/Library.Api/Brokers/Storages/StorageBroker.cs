//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using EFxceptions;
using Library.Api.Models.Books;
using Library.Api.Models.Readers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Library.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected IQueryable<T> SelectAll<T>() where T : class =>
            this.Set<T>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w =>
                w.Ignore(RelationalEventId.PendingModelChangesWarning));

            string connectionString =
                this.configuration.GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        public override void Dispose() { }

        //BOOKS
        async ValueTask<Book> IStorageBroker.InsertBookAsync(Book book)
        {
            this.Entry(book).State = EntityState.Added;
            await this.SaveChangesAsync();

            return book;
        }

        async ValueTask<Book> IStorageBroker.SelectBookByIdAsync(Guid bookId) =>
            await this.Books
                .FirstOrDefaultAsync(book => book.Id == bookId);

        IQueryable<Book> IStorageBroker.SelectAllBooks() =>
            SelectAll<Book>();

        async ValueTask<Book> IStorageBroker.UpdateBookAsync(Book book)
        {
            this.Entry(book).State = EntityState.Added;
            await this.SaveChangesAsync();

            return book;
        }

        async ValueTask<Book> IStorageBroker.DeleteBookAsync(Book book)
        {
            this.Entry(book).State = EntityState.Added;
            await this.SaveChangesAsync();

            return book;
        }

        //READERS
        async ValueTask<Reader> IStorageBroker.InsertReaderAsync(Reader reader)
        {
            this.Entry(reader).State = EntityState.Added;
            await this.SaveChangesAsync();

            return reader;
        }
    }
}
