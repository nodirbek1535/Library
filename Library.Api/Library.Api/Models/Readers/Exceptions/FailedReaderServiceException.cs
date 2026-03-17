//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class FailedReaderServiceException : Xeption
    {
        public FailedReaderServiceException(Exception innerException)
            : base(message: "Failed reader service error occurred, contact support.", innerException)
        { }
    }
}
