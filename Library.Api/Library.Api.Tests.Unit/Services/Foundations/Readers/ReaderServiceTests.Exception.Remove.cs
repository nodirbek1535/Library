//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Guid someReaderId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedReaderStorageException =
                new FailedReaderStorageException(sqlException);

            var expectedReaderDependencyException =
                new ReaderDependencyException(failedReaderStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(someReaderId))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Reader> removeReaderTask =
                this.readerService.RemoveReaderByIdAsync(someReaderId);

            //then
            await Assert.ThrowsAsync<ReaderDependencyException>(() =>
                removeReaderTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedReaderDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(someReaderId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveIfDbUpdateConcurrencyException()
        {
            //given
            Guid someReaderId = Guid.NewGuid();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedReaderException =
                new LockedReaderException(dbUpdateConcurrencyException);

            var expectedReaderDependencyValidationException =
                new ReaderDependencyValidationException(lockedReaderException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(someReaderId))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            //when
            ValueTask<Reader> removeReaderTask =
                this.readerService.RemoveReaderByIdAsync(someReaderId);

            //then
            await Assert.ThrowsAsync<ReaderDependencyValidationException>(() =>
                removeReaderTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReaderDependencyValidationException))),
                        Times.Once);
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveIfDbUpdateErrorOccursAndLogItAsync()
        {
            //given
            Guid someReaderId = Guid.NewGuid();
            var dbUpdateException = new DbUpdateException();

            var failedReaderStorageException =
                new FailedReaderStorageException(dbUpdateException);

            var expectedReaderDependencyException =
                new ReaderDependencyException(failedReaderStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(someReaderId))
                    .ThrowsAsync(dbUpdateException);

            //when
            ValueTask<Reader> removeReaderTask =
                this.readerService.RemoveReaderByIdAsync(someReaderId);

            //then
            await Assert.ThrowsAsync<ReaderDependencyException>(() =>
                removeReaderTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReaderDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(someReaderId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveIfServiceErrorOccursAndLofItAsync()
        {
            //given
            Guid someReaderId = Guid.NewGuid();
            var exception = new Exception();

            var failedReaderServiceException =
                new FailedReaderServiceException(exception);

            var expectedReaderServiceException =
                new ReaderServiceException(failedReaderServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(someReaderId))
                    .ThrowsAsync(exception);

            //when
            ValueTask<Reader> removeReaderTask =
                this.readerService.RemoveReaderByIdAsync(someReaderId);

            //then
            await Assert.ThrowsAsync<ReaderServiceException>(() =>
                removeReaderTask.AsTask());

            this.loggingBrokerMock.Verify(brokers =>
                brokers.LogError(It.Is(SameExceptionAs(
                    expectedReaderServiceException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(someReaderId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
