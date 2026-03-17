//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowSqlExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
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
            ValueTask<Reader> retrieveReaderByIdTask =
                this.readerService.RetrieveReaderByIdAsync(someReaderId);

            //then
            await Assert.ThrowsAsync<ReaderDependencyException>(() =>
                retrieveReaderByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(someReaderId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedReaderDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServicExceptionOnRetrieveByIdIfServiceErrorOccursAndLogitAsync()
        {
            //given
            Guid someReaderId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedReaderServiceException =
                new FailedReaderServiceException(serviceException);

            var expectedReaderServiceException =
                new ReaderServiceException(failedReaderServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(someReaderId))
                    .ThrowsAsync(serviceException);

            //when
            ValueTask<Reader> retrieveReaderByIdTask =
                this.readerService.RetrieveReaderByIdAsync(someReaderId);

            //then
            await Assert.ThrowsAsync<ReaderServiceException>(() =>
                retrieveReaderByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(someReaderId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReaderServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
