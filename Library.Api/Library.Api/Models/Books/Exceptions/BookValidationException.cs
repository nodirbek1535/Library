//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class BookValidationException:Xeption
    {
        public BookValidationException(Xeption innerException)
            : base(message: "Book validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
