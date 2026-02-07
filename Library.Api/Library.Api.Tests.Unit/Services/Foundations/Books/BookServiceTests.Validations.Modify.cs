//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
