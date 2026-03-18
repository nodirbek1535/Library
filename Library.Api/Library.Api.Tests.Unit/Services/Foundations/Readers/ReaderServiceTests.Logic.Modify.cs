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
        public async Task ShouldModifyReaderAsync()
        {
            //given
            Reader randomReader = CreateRandomReader();
            Reader inputReader = randomReader;
            Reader persistedReader = inputReader;
            Reader updateReader = inputReader;
            Reader expectedReader = updateReader;
            Guid readerId = inputReader.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(readerId))
                    .ReturnsAsync(persistedReader);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateReaderAsync(inputReader))
                    .ReturnsAsync(updateReader);

            //when
            Reader actualReader =
                await this.readerService.ModifyReaderAsync(inputReader);

            //then
            actualReader.Should().BeEquivalentTo(expectedReader);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(readerId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateReaderAsync(inputReader),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
