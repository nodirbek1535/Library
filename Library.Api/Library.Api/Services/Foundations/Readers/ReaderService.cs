//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Brokers.Loggings;
using Library.Api.Brokers.Storages;
using Library.Api.Models.Readers;

namespace Library.Api.Services.Foundations.Readers
{
    public partial class ReaderService: IReaderService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public ReaderService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Reader> AddReaderAsync(Reader reader) =>
            throw new NotImplementedException();
    }
}
