//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class BookDependencyValidationException:Xeption
    {
        public BookDependencyValidationException(Xeption innerException)
            : base("Book dependency validation error occurred, fix the errors and try again.", innerException)
        { }
    }
}
