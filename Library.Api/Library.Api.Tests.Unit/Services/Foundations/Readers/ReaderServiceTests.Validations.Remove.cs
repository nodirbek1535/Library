//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationsExceptionOnRemoveIfReaderDoesNotExistAndLogItAsync()
        {
            //given
            Guid randomReaderId = Guid.NewGuid();
            Guid inputReaderId = randomReaderId;
            Reader noReader = null;

            var notFoundReaderException =
                new NotFoundReaderException(inputReaderId);

            var expectedReaderValidationException =
                new ReaderValidationException(notFoundReaderException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(inputReaderId))
                    .ReturnsAsync(noReader);

            //when
            ValueTask<Reader> removeReaderTask =
                this.readerService.RemoveReaderByIdAsync(inputReaderId);

            //then
            await Assert.ThrowsAsync<ReaderValidationException>(() =>
                removeReaderTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(inputReaderId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedReaderValidationException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
