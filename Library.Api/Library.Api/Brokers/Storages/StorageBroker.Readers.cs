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
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Reader> readerEntityEntry =
                await broker.Readers.AddAsync(reader);

            await broker.SaveChangesAsync();

            return readerEntityEntry.Entity;
        }
    }
}
