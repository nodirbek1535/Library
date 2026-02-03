//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using FluentAssertions;
using Force.DeepCloner;
using Library.Api.Models.Books;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldAddBookAsync()
        {
            //given
            Book randomBook = CreateRandomBook();
            Book inputBook = randomBook;
            Book returningBook = inputBook;
            Book expectedBook = returningBook.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertBookAsync(inputBook))
                    .ReturnsAsync(returningBook);

            //when
            Book actualBook =
                await this.bookService.AddBookAsync(inputBook);

            //then
            actualBook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBookAsync(inputBook),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
