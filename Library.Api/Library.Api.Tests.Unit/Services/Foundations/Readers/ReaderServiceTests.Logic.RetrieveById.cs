//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using FluentAssertions;
using Library.Api.Models.Readers;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveReaderByIdAsync()
        {
            //given
            Guid randomReaderId = Guid.NewGuid();
            Guid inputReaderId = randomReaderId;

            Reader randomReader = CreateRandomReader();
            Reader persistedReader = randomReader;
            Reader expectedReader = persistedReader;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(inputReaderId))
                    .ReturnsAsync(persistedReader);

            //when
            Reader actualReader =
                await this.readerService.RetrieveReaderByIdAsync(inputReaderId);

            //then
            actualReader.Should().BeEquivalentTo(expectedReader);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(inputReaderId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
