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
        public async Task ShouldThrowRetrieveAllReaders()
        {
            //given
            IQueryable<Reader> randomReaders = CreateRandomReaders();
            IQueryable<Reader> persistedReaders = randomReaders;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllReaders())
                    .Returns(persistedReaders);

            //when
            IQueryable<Reader> actualReaders =
                this.readerService.RetrieveAllReaders();

            //them
            actualReaders.Should().BeEquivalentTo(persistedReaders);

            this.storageBrokerMock.Verify(broker =>
                    broker.SelectAllReaders(),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
