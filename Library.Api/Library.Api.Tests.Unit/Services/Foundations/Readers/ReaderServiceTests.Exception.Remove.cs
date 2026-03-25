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
    }
}
