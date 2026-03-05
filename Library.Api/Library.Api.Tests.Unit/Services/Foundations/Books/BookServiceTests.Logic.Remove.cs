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
        public async Task ShouldThrowRemoveBookByIdAsync()
        {
            //given
            Book randomBook = CreateRandomBook();
            Book storedBook = randomBook;
            Book deletedBook = storedBook;
            Guid bookId = storedBook.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(bookId))
                    .ReturnsAsync(storedBook);

             this.storageBrokerMock.Setup(broker =>
                broker.DeleteBookAsync(deletedBook))
                    .ReturnsAsync(deletedBook);

            //when
            Book actualBook =
                await this.bookService.RemoveBookByIdAsync(bookId);

            //then
            actualBook.Should().BeEquivalentTo(deletedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(bookId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteBookAsync(deletedBook),
                    Times.Once);

             this.storageBrokerMock.VerifyNoOtherCalls();
             this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
