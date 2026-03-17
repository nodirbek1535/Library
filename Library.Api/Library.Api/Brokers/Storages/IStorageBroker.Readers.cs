//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Readers;

namespace Library.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public ValueTask<Reader> InsertReaderAsync(Reader reader);
        public ValueTask<Reader> SelectReaderByIdAsync(Guid readerId);
    }
}
