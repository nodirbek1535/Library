//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Library.Api.Brokers.Loggings;
using Library.Api.Brokers.Storages;
using Library.Api.Models.Readers;
using Library.Api.Services.Foundations.Readers;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ReaderService readerService;

        public ReaderServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.readerService =
                new ReaderService(
                    storageBroker: this.storageBrokerMock.Object,
                    loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Reader CreateRandomReader() =>
            CreateReaderFiller().Create();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Filler<Reader> CreateReaderFiller()
        {
            var filler = new Filler<Reader>();

            filler.Setup()
                .OnType<Guid>().Use(Guid.NewGuid());

            return filler;
        }
    }
}
