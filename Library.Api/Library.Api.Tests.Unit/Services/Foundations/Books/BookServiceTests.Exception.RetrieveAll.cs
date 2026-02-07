//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Xeptions;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccursAndLogItAsync()
        {
            //given
            SqlException sqlException = GetSqlError();

            var failedBookServiceException =
                new FailedBookServiceException(sqlException);

            var expectedBookDependencyException =
                new BookDependencyException(failedBookServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllBooks())
                    .Throws(sqlException);

            //when
            Action retrieveAllBooksAction = () =>
                this.bookService.RetrieveAllBooks();

            //then
            BookDependencyException actualBookDependencyException =
                Assert.Throws<BookDependencyException>(retrieveAllBooksAction);

            actualBookDependencyException.SameExceptionAs(
                expectedBookDependencyException).Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllBooks(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedBookDependencyException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfSqlErrorOccursAndLogItAsync()
        {
            //given
            var serviceException = new Exception();

            var failedBookServiceException =
                new FailedBookServiceException(serviceException);

            var expectedBookServiceException =
                new BookServiceException(failedBookServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllBooks())
                    .Throws(serviceException);

            //when
            Action retrieveAllBooksAction = () =>
                this.bookService.RetrieveAllBooks();

            //then
            BookServiceException actualException =
                Assert.Throws<BookServiceException>(retrieveAllBooksAction);

            actualException
                .SameExceptionAs(expectedBookServiceException)
                .Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllBooks(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedBookServiceException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
