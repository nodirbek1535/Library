//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Brokers.Storages;
using Library.Api.Models.Books;
using Library.Api.Services.Foundations.Books;
using Moq;
using Tynamix.ObjectFiller;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IBookService bookService;

        public BookServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.bookService = 
                new BookService(storageBroker: this.storageBrokerMock.Object);
        }
        private static Book CreateRandomBook() =>
            CreateBookFiller().Create();

        private static Filler<Book> CreateBookFiller()
        {
            var filler = new Filler<Book>();

            filler.Setup()
                .OnType<Guid>().Use(Guid.NewGuid());

            return filler;
        }
    }
}
