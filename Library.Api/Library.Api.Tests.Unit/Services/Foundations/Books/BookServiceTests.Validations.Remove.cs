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
        public async Task ShouldThrowValidationExceptionOnRemoveIfBookIdIsInvalidAndLogItAsync()
        {
            //given
            Guid invalidBookId = Guid.Empty;

            var invalidBookException =
                new InvalidBookException();

            invalidBookException.AddData(
                key: nameof(Book.Id),
                values: "Id is required");

            var expectedBookValidationException =
                new BookValidationException(invalidBookException);

            //when
            ValueTask<Book> removeBookTask =
                this.bookService.RemoveBookByIdAsync(invalidBookId);

            //then
            await Assert.ThrowsAsync<BookValidationException>(() =>
                removeBookTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedBookValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

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
