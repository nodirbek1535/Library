//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class AlreadyExistBookException:Xeption
    {
        public AlreadyExistBookException(Exception innerException)
            : base("Book with the same identity already exists.", innerException)
        { }
    }
}
