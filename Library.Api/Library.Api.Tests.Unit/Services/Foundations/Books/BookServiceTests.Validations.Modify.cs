//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using FluentAssertions;
using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;
using Moq;
using Xeptions;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfBookIsNullAndLogItAsync()
        {
            //given
            Book nullBook = null;

            var nullBookException = new NullBookException();

            var excpectedBookValidationException =
                new BookValidationException(nullBookException);

            //when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(nullBook);

            BookValidationException actualBookValidationException =
                await Assert.ThrowsAsync<BookValidationException>(
                    modifyBookTask.AsTask);

            //then
            actualBookValidationException
                .SameExceptionAs(excpectedBookValidationException)
                    .Should().BeTrue();

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    excpectedBookValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfBookIsInvalidAndLogItAsync()
        {
            //given
            var invalidBook = new Book
            {
                Id = Guid.Empty
            };

            var invalidBookException = new InvalidBookException();

            invalidBookException.AddData(
                key: nameof(Book.Id),
                values: "Id is required");

            var expectedBookValidationException =
                new BookValidationException(invalidBookException);

            //when
            BookValidationException actualBookValidationException =
                await Assert.ThrowsAsync<BookValidationException>(
                    () => this.bookService.ModifyBookAsync(invalidBook).AsTask());

            //then
            actualBookValidationException
                .SameExceptionAs(expectedBookValidationException)
                    .Should().BeTrue();

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfBookDoesNotExistAAndLogItAsync()
        {
            //given
            Book randomBook = CreateRandomBook();
            Book nonExistingBook = randomBook;
            Book nullBook = null;

            var notFoundBookException =
                new NotFoundBookException(nonExistingBook.Id);

            var expectedBookValidationException =
                new BookValidationException(notFoundBookException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(nonExistingBook.Id))
                    .ReturnsAsync(nullBook);

            //when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(nonExistingBook);

            BookValidationException actualBookValidationException =
                await Assert.ThrowsAsync<BookValidationException>(
                    modifyBookTask.AsTask);

            //then
            actualBookValidationException
                .SameExceptionAs(expectedBookValidationException)
                    .Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(nonExistingBook.Id),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
