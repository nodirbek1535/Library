//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Brokers.Loggings;
using Library.Api.Brokers.Storages;
using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;

namespace Library.Api.Services.Foundations.Readers
{
    public partial class ReaderService : IReaderService
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

        public async ValueTask<Reader> AddReaderAsync(Reader reader)
        {
            try
            {
                if (reader is null)
                {
                    throw new NullReaderException();
                }

                return await this.storageBroker.InsertReaderAsync(reader);
            }
            catch (NullReaderException nullReaderException)
            {
                var readerValidationException =
                    new ReaderValidationException(nullReaderException);

                this.loggingBroker.LogError(readerValidationException);

                throw readerValidationException;
            }
        }
    }
}
