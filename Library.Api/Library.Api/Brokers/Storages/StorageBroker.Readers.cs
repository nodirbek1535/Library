//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Readers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Reader> Readers { get; set; }

        public async ValueTask<Reader> InsertReaderAsync(Reader reader)
        {
            EntityEntry<Reader> readerEntityEntry =
                await this.Readers.AddAsync(reader);

            await this.SaveChangesAsync();

            return readerEntityEntry.Entity;
        }

        public async ValueTask<Reader> SelectReaderByIdAsync(Guid readerId) =>
            await this.Readers.FirstOrDefaultAsync(r => r.Id == readerId);

        public IQueryable<Reader> SelectAllReaders() =>
            SelectAll<Reader>();
    }
}
