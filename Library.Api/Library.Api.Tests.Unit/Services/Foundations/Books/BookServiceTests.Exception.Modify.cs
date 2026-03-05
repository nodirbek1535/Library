using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xeptions;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Book someBook = CreateRandomBook();
            Guid bookId = someBook.Id;
            SqlException sqlException = GetSqlError();

            var failedBookStorageException =
                new FailedBookStorageException(sqlException);

            var expectedBookDependencyException =
                new BookDependencyException(failedBookStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(bookId))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(someBook);

            //then
            await Assert.ThrowsAsync<BookDependencyException>(() =>
                modifyBookTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(bookId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is
                (SameExceptionAs(expectedBookDependencyException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDbUpdateConcurrencyOccursAndLogItAsync()
        {
            //given
            Book someBook = CreateRandomBook();
            Guid bookId = someBook.Id;
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedBookException =
                new LockedBookException(dbUpdateConcurrencyException);

            var expectedBookDependencyValidationException =
                new BookDependencyValidationException(lockedBookException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(bookId))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            //when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(someBook);

            //then
            await Assert.ThrowsAsync<BookDependencyValidationException>(() =>
                modifyBookTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(bookId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is
                    (SameExceptionAs(expectedBookDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDbUpdateErrorOccursAndLogItAsync()
        {
            //given
            Book someBook = CreateRandomBook();
            Guid bookId = someBook.Id;
            var dbUpdateException = new DbUpdateException();

            var failedBookStorageException =
                new FailedBookStorageException(
                    new Xeption(message: "Failed book storage error occured.", dbUpdateException));

            var expectedBookDependencyException =
                new BookDependencyException(failedBookStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(bookId))
                    .ThrowsAsync(dbUpdateException);

            //when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(someBook);

            //then
            await Assert.ThrowsAsync<BookDependencyException>(() =>
                modifyBookTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(bookId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is
                    (SameExceptionAs(expectedBookDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task SHouldThrowServiceExceptionOnModifyIfServiceErrorOccursAndLogItAsync()
        {
            //given
            Book someBook = CreateRandomBook();
            Guid bookId = someBook.Id;
            var serviceException = new Exception();

            var failedBookServiceException =
                new FailedBookServiceException(serviceException);

            var expectedBookServiceException =
                new BookServiceException(failedBookServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(bookId))
                    .ThrowsAsync(serviceException);

            //when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(someBook);

            //then
            await Assert.ThrowsAsync<BookServiceException>(() =>
                modifyBookTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(bookId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is
                    (SameExceptionAs(expectedBookServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
