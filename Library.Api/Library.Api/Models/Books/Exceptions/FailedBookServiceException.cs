//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class FailedBookServiceException:Xeption
    {
        public FailedBookServiceException(Exception innerException)
            : base("Failed book service error occurred, contact support.", innerException)
        { }
    }
}
