//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Library.Api.Models.Books;
using Microsoft.AspNetCore.Components;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveBookByIdAsync()
        {
            //given
            Guid randomBookId = Guid.NewGuid();
            Guid inputBookId = randomBookId;

            Book randomBook = CreateRandomBook();
            Book persistedBook = randomBook;
            Book expectedBook = persistedBook;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(inputBookId))
                    .ReturnsAsync(persistedBook);

            //when
            Book actualBook =
                await this.bookService.RetrieveBookByIdAsync(inputBookId);

            //then
            actualBook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(inputBookId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();    
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
