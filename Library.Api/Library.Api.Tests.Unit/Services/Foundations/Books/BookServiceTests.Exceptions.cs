//===============================================
//@nodirbek1535 library api program (C)
//===============================================

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
        public async Task ShouldThrowCriticalDependencyValidationExceptionOnAddSqlErrorOccursAndLogItAsync()
        {
            //given 
            Book someBook = CreateRandomBook();
            SqlException sqlException = GetSqlError();
            var failedBookStorageException = new FailedBookStorageException(sqlException);

            var expectedBookDependencyException =
                new BookDependencyException(failedBookStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertBookAsync(someBook))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Book> addBookTask =
                this.bookService.AddBookAsync(someBook);

            //then
            await Assert.ThrowsAsync<BookDependencyException>(() =>
                addBookTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBookAsync(someBook),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedBookDependencyException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
