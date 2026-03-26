//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Books;
using Library.Api.Models.Readers;

namespace Library.Api.Models.ReaderBooks
{
    public class ReaderBook
    {
        public Reader Reader { get; set; } = default!;
        public List<Book> Books { get; set; } = new();
    }
}
