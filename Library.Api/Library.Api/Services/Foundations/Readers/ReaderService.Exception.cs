//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace Library.Api.Services.Foundations.Readers
{
    public partial class ReaderService
    {
        private delegate ValueTask<Reader> ReturningReaderFunction();

        private async ValueTask<Reader> TryCatch(ReturningReaderFunction returningReaderFunction)
        {
            try
            {
                return await returningReaderFunction();
            }
            catch (NullReaderException nullReaderException)
            {
                throw CreateAndLogValidationException(nullReaderException);
            }
            catch (InvalidReaderException invalidReaderException)
            {
                throw CreateAndLogValidationException(invalidReaderException);
            }
            catch (SqlException sqlException)
            {
                var failedReaderStorageException =
                    new FailedReaderStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedReaderStorageException);
            }
            catch (Exception exception)
            {
                var failedReaderServiceException =
                    new FailedReaderStorageException(exception);
                throw CreateAndLogCriticalDependencyException(failedReaderServiceException);
            }
        }
        private ReaderValidationException CreateAndLogValidationException(Xeption exception)
        {
            var readerValidationException =
                new ReaderValidationException(exception);

            this.loggingBroker.LogError(readerValidationException);

            return readerValidationException;
        }

        private ReaderDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var readerDependencyException =
                new ReaderDependencyException(exception);

            this.loggingBroker.LogCritical(readerDependencyException);

            return readerDependencyException;
        }
    }
}