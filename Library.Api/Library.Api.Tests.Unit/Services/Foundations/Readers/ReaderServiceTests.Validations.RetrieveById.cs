using System;
using System.Collections.Generic;
using System.Text;
using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            //given
            Guid invalidReaderId = Guid.Empty;

            var invalidReaderException = new InvalidReaderException();

            invalidReaderException.AddData(
                key: nameof(Reader.Id),
                values: "Id is required.");

            var expectedReaderValidationException = new ReaderValidationException(invalidReaderException);

            //when
            ValueTask<Reader> retrieveReaderByIdTask =
                this.readerService.RetrieveReaderByIdAsync(invalidReaderId);

            //then
            await Assert.ThrowsAsync<ReaderValidationException>(() =>
                retrieveReaderByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(invalidReaderId),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedReaderValidationException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
