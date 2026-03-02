using System;
using System.Collections.Generic;
using System.Text;
using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

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
    }
}
