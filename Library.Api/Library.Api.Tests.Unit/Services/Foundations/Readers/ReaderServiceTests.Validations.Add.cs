//===============================================
//@nodirbek1535 library api program (C)
//===============================================

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
        public async Task ShouldThrowValidationExceptionOnAddIfReaderIsNullAndLogItAsync()
        {
            //given
            Reader nullReader = null;
            var nullReaderException = new NullReaderException();

            var expectedReaderValidationException =
                new ReaderValidationException(nullReaderException);

            //when
            ValueTask<Reader> addReaderTask =
                this.readerService.AddReaderAsync(nullReader);

            //then
            await Assert.ThrowsAsync<ReaderValidationException>(() =>
                addReaderTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReaderAsync(It.IsAny<Reader>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is
                (SameExceptionAs(expectedReaderValidationException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfReaderIsInvalidAndLogItAsync(string invalidText)
        {
            //given
            var invalidReader = new Reader  
            {
                Id = Guid.Empty,
                FirstName = invalidText,
                LastName = invalidText,
                Age = 0
            };

            var invalidReaderException = new InvalidReaderException();

            invalidReaderException.AddData(
                key: nameof(Reader.Id),
                values: "Id is required.");

            invalidReaderException.AddData(
                key: nameof(Reader.FirstName),
                values: "Text is required."); 

            invalidReaderException.AddData(
                key: nameof(Reader.LastName),
                values: "Text is required.");

            invalidReaderException.AddData(
                key: nameof(Reader.Age),
                values: "Number is required and should be greater than zero.");

            var expectedReaderValidationException =
                new ReaderValidationException(invalidReaderException);

            //when
            ValueTask<Reader> addReaderTask =
                this.readerService.AddReaderAsync(invalidReader);

            //then
            await Assert.ThrowsAsync<ReaderValidationException>(() =>
                addReaderTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is
                (SameExceptionAs(expectedReaderValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReaderAsync(It.IsAny<Reader>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
