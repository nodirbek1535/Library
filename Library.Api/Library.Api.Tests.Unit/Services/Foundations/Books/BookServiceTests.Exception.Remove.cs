//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Guid someBookId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedBookStorageException =
                new FailedBookStorageException(sqlException);

            var expectedBookDependencyException =
                new BookDependencyException(failedBookStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(someBookId))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Book> removeBookTask =
                this.bookService.RemoveBookByIdAsync(someBookId);

            //then
            await Assert.ThrowsAsync<BookDependencyException>(() =>
                removeBookTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedBookDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(someBookId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveIfDbUpdateConcurrencyException()
        {
            //given
            Guid someBookId = Guid.NewGuid();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedBookException =
                new LockedBookException(dbUpdateConcurrencyException);

            var expectedBookDependencyValidationException =
                new BookDependencyValidationException(lockedBookException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(someBookId))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            //when
            ValueTask<Book> removeBookTask =
                this.bookService.RemoveBookByIdAsync(someBookId);

            //then
            await Assert.ThrowsAsync<BookDependencyValidationException>(() =>
                removeBookTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookDependencyValidationException))),
                        Times.Once);
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveIfDbUpdateErrorOccursAndLogItAsync()
        {
            //given
            Guid someBookId = Guid.NewGuid();
            var dbUpdateException = new DbUpdateException();

            var failedBookStorageException =
                new FailedBookStorageException(dbUpdateException);

            var expectedBookDependencyException =
                new BookDependencyException(failedBookStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(someBookId))
                    .ThrowsAsync(dbUpdateException);

            //when
            ValueTask<Book> removeBookTask =
                this.bookService.RemoveBookByIdAsync(someBookId);

            //then
            await Assert.ThrowsAsync<BookDependencyException>(() =>
                removeBookTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(someBookId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveIfServiceErrorOccursAndLofItAsync()
        {
            //given
            Guid someBookId = Guid.NewGuid();   
            var exception = new Exception();

            var failedBookServiceException =
                new FailedBookServiceException(exception);

            var expectedBookServiceException =
                new BookServiceException(failedBookServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(someBookId))
                    .ThrowsAsync(exception);

            //when
            ValueTask<Book> removeBookTask =
                this.bookService.RemoveBookByIdAsync(someBookId);

            //then
            await Assert.ThrowsAsync<BookServiceException>(() =>
                removeBookTask.AsTask());

            this.loggingBrokerMock.Verify(brokers =>
                brokers.LogError(It.Is(SameExceptionAs(
                    expectedBookServiceException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(someBookId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
