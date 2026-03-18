//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System.Linq.Expressions;
using System.Runtime.Serialization;
using Library.Api.Brokers.Loggings;
using Library.Api.Brokers.Storages;
using Library.Api.Models.Readers;
using Library.Api.Services.Foundations.Readers;
using Microsoft.Data.SqlClient;
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

        private static IQueryable<Reader> CreateRandomReaders()
        {
            int radnomNumber = GetRandomNumber();

            return Enumerable.Range(0, radnomNumber)
                .Select(_ => CreateRandomReader())
                .AsQueryable();
        }

        public static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        #pragma warning disable SYSLIB0050 // Do not catch general exception types
        private static SqlException GetSqlError() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));
        #pragma warning restore SYSLIB0050 // Do not catch general exception types

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
