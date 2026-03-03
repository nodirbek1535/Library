//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationsExceptionOnRemoveIfBookDoesNotExistAndLogItAsync()
        {
            //given
            Guid randomBookId = Guid.NewGuid();
            Guid inputBookId = randomBookId;
            Book noBook = null;

            var notFoundBookException =
                new NotFoundBookException(inputBookId);

            var expectedBookValidationException =
                new BookValidationException(notFoundBookException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(inputBookId))
                    .ReturnsAsync(noBook);

            //when
            ValueTask<Book> removeBookTask =
                this.bookService.RemoveBookByIdAsync(inputBookId);

            //then
            await Assert.ThrowsAsync<BookValidationException>(() =>
                removeBookTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(inputBookId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedBookValidationException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
