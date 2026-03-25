//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Brokers.Loggings;
using Library.Api.Brokers.Storages;
using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Microsoft.Data.SqlClient;

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

        public ValueTask<Reader> AddReaderAsync(Reader reader) =>
            TryCatch(async () =>
            {
                ValidateReaderOnAdd(reader);

                return await this.storageBroker.InsertReaderAsync(reader);
            });

        public ValueTask<Reader> RetrieveReaderByIdAsync(Guid readerId) =>
            TryCatch(async () =>
            {
                ValidateReaderId(readerId);
                Reader maybeReader = await this.storageBroker.SelectReaderByIdAsync(readerId);
                ValidateStorageReader(maybeReader, readerId);

                return maybeReader;
            });

        public IQueryable<Reader> RetrieveAllReaders()
        {
            try
            {
                return this.storageBroker.SelectAllReaders();
            }
            catch (SqlException sqlException)
            {
                var failedReaderServiceException =
                    new FailedReaderServiceException(sqlException);

                var readerDependencyException =
                    new ReaderDependencyException(failedReaderServiceException);

                this.loggingBroker.LogCritical(readerDependencyException);

                throw readerDependencyException;
            }
            catch (Exception exception)
            {
                var failedReaderServiceException =
                    new FailedReaderServiceException(exception);

                var readerServiceException =
                    new ReaderServiceException(failedReaderServiceException);

                this.loggingBroker.LogError(readerServiceException);

                throw readerServiceException;
            }
        }

        public ValueTask<Reader> ModifyReaderAsync(Reader reader) =>
            TryCatch(async () =>
            {
                ValidateReaderOnModify(reader);

                Reader maybeReader =
                    await this.storageBroker.SelectReaderByIdAsync(reader.Id);

                ValidateStorageReader(maybeReader, reader.Id);

                return await this.storageBroker.UpdateReaderAsync(reader);
            });

        public ValueTask<Reader> RemoveReaderByIdAsync(Guid readerId) =>
            TryCatch(async () =>
            {
                ValidateReaderId(readerId);

                Reader maybeReader =
                    await this.storageBroker.SelectReaderByIdAsync(readerId);

                ValidateStorageReader(maybeReader, readerId);

                return await this.storageBroker.DeleteReaderAsync(maybeReader);
            });
    }
}
