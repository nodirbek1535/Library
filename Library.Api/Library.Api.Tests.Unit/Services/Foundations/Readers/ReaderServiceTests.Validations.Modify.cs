//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Moq;
using Xeptions;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfReaderIsNullAndLogItAsync()
        {
            //given
            Reader nullReader = null;

            var nullReaderException = new NullReaderException();

            var excpectedReaderValidationException =
                new ReaderValidationException(nullReaderException);

            //when
            ValueTask<Reader> modifyReaderTask =
                this.readerService.ModifyReaderAsync(nullReader);

            ReaderValidationException actualReaderValidationException =
                await Assert.ThrowsAsync<ReaderValidationException>(
                    modifyReaderTask.AsTask);

            //then
            actualReaderValidationException
                .SameExceptionAs(excpectedReaderValidationException)
                    .Should().BeTrue();

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    excpectedReaderValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfReaderIsInvalidAndLogItAsync()
        {
            //given
            var invalidReader = new Reader
            {
                Id = Guid.Empty
            };

            var invalidReaderException = new InvalidReaderException();

            invalidReaderException.AddData(
                key: nameof(Reader.Id),
                values: "Id is required.");

            var expectedReaderValidationException =
                new ReaderValidationException(invalidReaderException);

            //when
            ReaderValidationException actualReaderValidationException =
                await Assert.ThrowsAsync<ReaderValidationException>(
                    () => this.readerService.ModifyReaderAsync(invalidReader).AsTask());

            //then
            actualReaderValidationException
                .SameExceptionAs(expectedReaderValidationException)
                    .Should().BeTrue();

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReaderValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
