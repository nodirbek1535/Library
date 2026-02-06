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
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            //given
            Guid invalidBookId = Guid.Empty;

            var invalidBookException = new InvalidBookException();

            invalidBookException.AddData(
                key: nameof(Book.Id),
                values: "Id is required");

            var epectedBookValidationException = new BookValidationException(invalidBookException);

            //when
            ValueTask<Book> retrieveBookByIdTask =
                this.bookService.RetrieveBookByIdAsync(invalidBookId);

            //then
            await Assert.ThrowsAsync<BookValidationException>(() =>
                retrieveBookByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(invalidBookId),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(epectedBookValidationException))),
                    Times.Once);

             this.storageBrokerMock.VerifyNoOtherCalls();
             this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRetrieveByIdIfBookIsNorFoundAndLogItAsync()
        {
            //given
            Guid someBookId = Guid.NewGuid();
            Book noBook = null;

            var notFoundBookException = 
                new NotFoundBookException(someBookId);

            var expectedBookValidationException = 
                new BookValidationException(notFoundBookException);
             
            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(someBookId))
                    .ReturnsAsync(noBook);

            //when
            ValueTask<Book> retrieveBookByIdTask =
                this.bookService.RetrieveBookByIdAsync(someBookId);

            //then
            await Assert.ThrowsAsync<BookValidationException>(() =>
                retrieveBookByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(someBookId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedBookValidationException))),
                        Times.Once);

             this.storageBrokerMock.VerifyNoOtherCalls();
             this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
