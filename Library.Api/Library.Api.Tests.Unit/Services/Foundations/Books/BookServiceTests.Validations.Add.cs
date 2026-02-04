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
        public async Task ShouldThrowValidationExceptionOnAddIfBookIsNullAndLogItAsync()
        {
            //given
            Book nullBook = null;
            var nullBookException = new NullBookException();
            
            var expectedBookValidationException =
                new BookValidationException(nullBookException);

            //when
            ValueTask<Book> addBookTask = 
                this.bookService.AddBookAsync(nullBook);

            //then
            await Assert.ThrowsAsync<BookValidationException>(() =>
                addBookTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBookAsync(It.IsAny<Book>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfBookIsInvalidAndLogItAsync(string invalidTaxt)
        {
            //given
            var invalidBook = new Book
            {
                Name = invalidTaxt,
            };

            var invalidBookException = new InvalidBookException();

            invalidBookException.AddData(
                key: nameof(Book.Id),
                values: "Id is required");

            invalidBookException.AddData(
                key: nameof(Book.Name),
                values: "Text is required");

            invalidBookException.AddData(
                key: nameof(Book.Author),
                values: "Text is required");

            invalidBookException.AddData(
                key: nameof(Book.Genre),
                values: "Text is required");

            invalidBookException.AddData(
                key: nameof(Book.ReaderId),
                values: "Id is required");

            var expectedBookValidationException =
                new BookValidationException(invalidBookException);

            //when
            ValueTask<Book> addBookTask =
                this.bookService.AddBookAsync(invalidBook);

            //then
            await Assert.ThrowsAsync<BookValidationException>(() =>
                addBookTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBookAsync(It.IsAny<Book>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
