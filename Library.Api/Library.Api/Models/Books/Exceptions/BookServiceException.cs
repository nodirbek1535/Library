//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class BookServiceException:Xeption
    {
        public BookServiceException(Exception innerException)
            : base("Book service error occurred, contact support.", innerException)
        { } 
    }
}
