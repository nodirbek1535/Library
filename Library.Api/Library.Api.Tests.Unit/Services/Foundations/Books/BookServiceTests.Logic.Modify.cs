//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Library.Api.Models.Books;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldModifyBookAsync()
        {
            //given
            Book randomBook = CreateRandomBook();
            Book inputBook = randomBook;
            Book persistedBook = inputBook;
            Book updateBook = inputBook;
            Book expectedBook = updateBook;
            Guid bookId = inputBook.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(bookId))
                    .ReturnsAsync(persistedBook);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateBookAsync(inputBook))
                    .ReturnsAsync(updateBook);

            //when
            Book actualBook =
                await this.bookService.ModifyBookAsync(inputBook);

            //then
            actualBook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(bookId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateBookAsync(inputBook),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
