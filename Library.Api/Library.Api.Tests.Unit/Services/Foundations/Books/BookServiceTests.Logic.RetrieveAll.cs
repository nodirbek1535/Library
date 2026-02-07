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
        public async Task ShouldThrowRetrieveAllBooks()
        {
            //given
            IQueryable<Book> randomBooks = CreateRandomBooks();
            IQueryable<Book> persistedBooks = randomBooks;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllBooks())
                    .Returns(persistedBooks);

            //when
            IQueryable<Book> actualBooks =
                this.bookService.RetrieveAllBooks();

            //them
            actualBooks.Should().BeEquivalentTo(persistedBooks);

            this.storageBrokerMock.Verify(broker =>
                    broker.SelectAllBooks(),
                        Times.Once);
    
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
