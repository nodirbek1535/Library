//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class ReaderServiceException : Xeption
    {
        public ReaderServiceException(Exception innerException)
            : base(message: "Reader service error occurred, contact support.", innerException)
        { }
    }
}
