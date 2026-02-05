//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class InvalidBookException:Xeption
    {
        public InvalidBookException()
            : base("Book is invalid.")
        { } 
    }
}
