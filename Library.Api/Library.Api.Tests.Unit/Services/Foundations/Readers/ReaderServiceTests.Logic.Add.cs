//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Force.DeepCloner;
using Library.Api.Models.Readers;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldAddReaderAsync()
        {
            //given
            Reader randomReader = CreateRandomReader();
            Reader inputReader = randomReader;
            Reader storageReader = inputReader;
            Reader expectedReader = storageReader.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertReaderAsync(inputReader))
                    .ReturnsAsync(storageReader);

            //when
            Reader actualReader =
                await this.readerService.AddReaderAsync(inputReader);

            //then
            actualReader.Should().BeEquivalentTo(expectedReader);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReaderAsync(inputReader),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
