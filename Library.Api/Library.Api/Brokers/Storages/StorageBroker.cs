//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using EFxceptions;
using Library.Api.Models.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected IQueryable<T> SelectAll<T>() where T : class => 
            this.Set<T>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                this.configuration.GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        public override void Dispose() { }

        //BOOKS
        async ValueTask<Book> IStorageBroker.InsertBookAsync(Book book)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(book).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return book;
        }

        async ValueTask<Book> IStorageBroker.SelectBookByIdAsync(Guid bookId)
        {
            var broker = new StorageBroker(this.configuration);

            return await broker.Books
                .FirstOrDefaultAsync(book => book.Id == bookId);
        }

        IQueryable<Book> IStorageBroker.SelectAllBooks() =>
            SelectAll<Book>();
    }
}
