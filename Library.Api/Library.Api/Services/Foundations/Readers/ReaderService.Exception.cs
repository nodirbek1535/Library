//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using EFxceptions.Models.Exceptions;
using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace Library.Api.Services.Foundations.Readers
{
    public partial class ReaderService
    {
        private delegate ValueTask<Reader> ReturningReaderFunction();
        private delegate IQueryable<Reader> ReturningReadersFunction();

        private async ValueTask<Reader> TryCatch(ReturningReaderFunction returningReaderFunction)
        {
            try
            {
                return await returningReaderFunction();
            }
            catch (NullReaderException ex)
            {
                throw CreateAndLogValidationException(ex);
            }
            catch (InvalidReaderException ex)
            {
                throw CreateAndLogValidationException(ex);
            }
            catch (NotFoundReaderException ex)
            {
                throw CreateAndLogValidationException(ex);
            }
            catch (SqlException ex)
            {
                var failedStorageException = new FailedReaderStorageException(ex);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (ForeignKeyConstraintConflictException ex)
            {
                var invalidReferenceException = new InvalidReaderReferenceException(ex);
                throw CreateAndLogDependencyValidationException(invalidReferenceException);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var lockedException = new LockedReaderException(ex);
                throw CreateAndLogDependencyValidationException(lockedException);
            }
            catch (DbUpdateException ex)
            {
                var failedStorageException = new FailedReaderStorageException(ex);
                throw CreateAndLogCriticalDependencyException(failedStorageException); // 🔥
            }
            catch (DuplicateKeyException ex)
            {
                var alreadyExistsException = new AlreadyExistReaderException(ex);
                throw CreateAndLogDependencyValidationException(alreadyExistsException);
            }
            catch (Exception ex)
            {
                var failedServiceException = new FailedReaderServiceException(ex);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private async ValueTask<Reader> TryCatchRemove(ReturningReaderFunction returningReaderFunction)
        {
            try
            {
                return await returningReaderFunction();
            }
            catch (NullReaderException ex)
            {
                throw CreateAndLogValidationException(ex);
            }
            catch (InvalidReaderException ex)
            {
                throw CreateAndLogValidationException(ex);
            }
            catch (NotFoundReaderException ex)
            {
                throw CreateAndLogValidationException(ex);
            }
            catch (SqlException ex)
            {
                var failedStorageException = new FailedReaderStorageException(ex);
                throw CreateAndLogCriticalDependencyException(failedStorageException); // 🔥
            }
            catch (ForeignKeyConstraintConflictException ex)
            {
                var invalidReferenceException = new InvalidReaderReferenceException(ex);
                throw CreateAndLogDependencyValidationException(invalidReferenceException);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var lockedException = new LockedReaderException(ex);
                throw CreateAndLogDependencyValidationException(lockedException);
            }
            catch (DbUpdateException ex)
            {
                var failedStorageException = new FailedReaderStorageException(ex);
                throw CreateAndLogDependencyException(failedStorageException); // ❗
            }
            catch (DuplicateKeyException ex)
            {
                var alreadyExistsException = new AlreadyExistReaderException(ex);
                throw CreateAndLogDependencyValidationException(alreadyExistsException);
            }
            catch (Exception ex)
            {
                var failedServiceException = new FailedReaderServiceException(ex);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private IQueryable<Reader> TryCatch(ReturningReadersFunction returningReadersFunction)
        {
            try
            {
                return returningReadersFunction();
            }
            catch (SqlException ex)
            {
                var failedStorageException =
                    new FailedReaderStorageException(ex);

                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception ex)
            {
                var failedServiceException =
                    new FailedReaderServiceException(ex);

                throw CreateAndLogServiceException(failedServiceException);
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

        private ReaderDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var readerDependencyValidationException =
                new ReaderDependencyValidationException(exception);
            this.loggingBroker.LogError(readerDependencyValidationException);
            return readerDependencyValidationException;
        }

        private ReaderServiceException CreateAndLogServiceException(Xeption exception)
        {
            var readerServiceException =
                new ReaderServiceException(exception);

            this.loggingBroker.LogError(readerServiceException);

            return readerServiceException;
        }

        private ReaderDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var readerDependencyException =
                new ReaderDependencyException(exception);

            this.loggingBroker.LogError(readerDependencyException);

            return readerDependencyException;
        }
    }
}
