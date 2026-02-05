//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class BookDependencyException:Xeption
    {
        public BookDependencyException(Xeption innerException)
            : base("Book dependency error occurred, contact support.", innerException)
        { }
    }
}
