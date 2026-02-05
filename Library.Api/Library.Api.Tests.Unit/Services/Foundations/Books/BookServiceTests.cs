//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System.Linq.Expressions;
using System.Runtime.Serialization;
using Library.Api.Brokers.Loggings;
using Library.Api.Brokers.Storages;
using Library.Api.Models.Books;
using Library.Api.Services.Foundations.Books;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;    
        private readonly IBookService bookService;

        public BookServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.bookService =
                new BookService(
                    storageBroker: this.storageBrokerMock.Object,
                    loggingBroker: this.loggingBrokerMock.Object);
        }
        private static Book CreateRandomBook() =>
            CreateBookFiller().Create();

        private static SqlException GetSqlError() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private Expression<Func<Exception, bool>> SameExceptionAs(
            Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static Filler<Book> CreateBookFiller()
        {
            var filler = new Filler<Book>();

            filler.Setup()
                .OnType<Guid>().Use(Guid.NewGuid());

            return filler;
        }
    }
}
