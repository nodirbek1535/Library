//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Library.Api.Models.Readers;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowRemoveReaderByIdAsync()
        {
            //given
            Reader randomReader = CreateRandomReader();
            Reader storedReader = randomReader;
            Reader deletedReader = storedReader;
            Guid readerId = storedReader.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(readerId))
                    .ReturnsAsync(storedReader);

            this.storageBrokerMock.Setup(broker =>
               broker.DeleteReaderAsync(deletedReader))
                   .ReturnsAsync(deletedReader);

            //when
            Reader actualReader =
                await this.readerService.RemoveReaderByIdAsync(readerId);

            //then
            actualReader.Should().BeEquivalentTo(deletedReader);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(readerId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteReaderAsync(deletedReader),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
