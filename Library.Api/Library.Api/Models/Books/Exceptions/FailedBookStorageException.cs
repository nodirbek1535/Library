//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class FailedBookStorageException:Xeption
    {
        public FailedBookStorageException(Exception innerException)
            : base("Failed book storage error occurred, contact support.", innerException)
        { }
    }
}
