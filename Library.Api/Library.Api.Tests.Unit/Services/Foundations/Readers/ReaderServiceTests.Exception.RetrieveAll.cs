//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Library.Api.Models.Readers.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Xeptions;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccursAndLogItAsync()
        {
            //given
            SqlException sqlException = GetSqlError();

            var failedReaderStorageException =
                new FailedReaderStorageException(sqlException);

            var expectedReaderDependencyException =
                new ReaderDependencyException(failedReaderStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllReaders())
                    .Throws(sqlException);

            //when
            Action retrieveAllReadersAction = () =>
                this.readerService.RetrieveAllReaders();

            //then
            ReaderDependencyException actualReaderDependencyException =
                Assert.Throws<ReaderDependencyException>(retrieveAllReadersAction);

            actualReaderDependencyException.SameExceptionAs(
                expectedReaderDependencyException).Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllReaders(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedReaderDependencyException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfSqlErrorOccursAndLogItAsync()
        {
            //given
            var serviceException = new Exception();

            var failedReaderServiceException =
                new FailedReaderServiceException(serviceException);

            var expectedReaderServiceException =
                new ReaderServiceException(failedReaderServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllReaders())
                    .Throws(serviceException);

            //when
            Action retrieveAllReadersAction = () =>
                this.readerService.RetrieveAllReaders();

            //then
            ReaderServiceException actualException =
                Assert.Throws<ReaderServiceException>(retrieveAllReadersAction);

            actualException
                .SameExceptionAs(expectedReaderServiceException)
                .Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllReaders(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedReaderServiceException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
