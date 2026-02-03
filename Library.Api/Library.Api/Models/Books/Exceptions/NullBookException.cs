//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class NullBookException : Xeption
    {
        public NullBookException()
            : base(message: "The book is null.")
        { }
    }
}
