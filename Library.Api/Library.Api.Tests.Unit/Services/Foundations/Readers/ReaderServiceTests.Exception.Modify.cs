//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xeptions;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Reader someReader = CreateRandomReader();
            Guid readerId = someReader.Id;
            SqlException sqlException = GetSqlError();

            var failedReaderStorageException =
                new FailedReaderStorageException(sqlException);

            var expectedReaderDependencyException =
                new ReaderDependencyException(failedReaderStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(readerId))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Reader> modifyReaderTask =
                this.readerService.ModifyReaderAsync(someReader);

            //then
            await Assert.ThrowsAsync<ReaderDependencyException>(() =>
                modifyReaderTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(readerId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is
                (SameExceptionAs(expectedReaderDependencyException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDbUpdateConcurrencyOccursAndLogItAsync()
        {
            //given
            Reader someReader = CreateRandomReader();
            Guid readerId = someReader.Id;
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedReaderException =
                new LockedReaderException(dbUpdateConcurrencyException);

            var expectedReaderDependencyValidationException =
                new ReaderDependencyValidationException(lockedReaderException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(readerId))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            //when
            ValueTask<Reader> modifyReaderTask =
                this.readerService.ModifyReaderAsync(someReader);

            //then
            await Assert.ThrowsAsync<ReaderDependencyValidationException>(() =>
                modifyReaderTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(readerId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is
                    (SameExceptionAs(expectedReaderDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDbUpdateErrorOccursAndLogItAsync()
        {
            //given
            Reader someReader = CreateRandomReader();
            Guid readerId = someReader.Id;
            var dbUpdateException = new DbUpdateException();

            var failedReaderStorageException =
                new FailedReaderStorageException(
                    new Xeption(message: "Failed reader storage error occured.", dbUpdateException));

            var expectedReaderDependencyException =
                new ReaderDependencyException(failedReaderStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(readerId))
                    .ThrowsAsync(dbUpdateException);

            //when
            ValueTask<Reader> modifyReaderTask =
                this.readerService.ModifyReaderAsync(someReader);

            //then
            await Assert.ThrowsAsync<ReaderDependencyException>(() =>
                modifyReaderTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(readerId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is
                    (SameExceptionAs(expectedReaderDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
