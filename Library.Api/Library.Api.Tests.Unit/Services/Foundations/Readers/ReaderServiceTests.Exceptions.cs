//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using EFxceptions.Models.Exceptions;
using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Reader someReader = CreateRandomReader();
            SqlException sqlException = GetSqlError();

            var failedReaderStorageException =
                new FailedReaderStorageException(sqlException);

            var expectedReaderDependencyException =
                new ReaderDependencyException(failedReaderStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertReaderAsync(someReader))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Reader> addReaderTask =
                this.readerService.AddReaderAsync(someReader);

            //then
            await Assert.ThrowsAsync<ReaderDependencyException>(() =>
                addReaderTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReaderAsync(someReader),
                Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedReaderDependencyException))),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            //given
            Reader someReader = CreateRandomReader();
            string someMessage = GetRandomString();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistReaderException =
                new AlreadyExistReaderException(duplicateKeyException);

            var readerDependencyValidationException =
                new ReaderDependencyValidationException(alreadyExistReaderException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertReaderAsync(someReader))
                    .ThrowsAsync(duplicateKeyException);

            //when
            ValueTask<Reader> addReaderTask =
                this.readerService.AddReaderAsync(someReader);

            //then
            await Assert.ThrowsAsync<ReaderDependencyValidationException>(() =>
                addReaderTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReaderAsync(someReader),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(readerDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given
            Reader someReader = CreateRandomReader();
            var serviceException = new Exception();

            var failedReaderServiceException =
                new FailedReaderServiceException(serviceException);

            var expectedReaderServiceException =
                new ReaderServiceException(failedReaderServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertReaderAsync(someReader))
                    .ThrowsAsync(serviceException);

            //when
            ValueTask<Reader> addReaderTask =
                this.readerService.AddReaderAsync(someReader);

            //then
            await Assert.ThrowsAsync<ReaderServiceException>(() =>
                addReaderTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReaderAsync(someReader),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedReaderServiceException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
