//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Readers;

namespace Library.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Reader> InsertReaderAsync(Reader reader);
        ValueTask<Reader> SelectReaderByIdAsync(Guid readerId);
        IQueryable<Reader> SelectAllReaders();
        ValueTask<Reader> UpdateReaderAsync(Reader reader);
    }
}
