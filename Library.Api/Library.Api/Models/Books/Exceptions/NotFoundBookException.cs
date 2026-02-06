//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class NotFoundBookException:Xeption
    {
        public NotFoundBookException(Guid bookId)
            : base(message: $"Couldn't find book with id: {bookId}")
        { }
    }
}
