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
    }
}
