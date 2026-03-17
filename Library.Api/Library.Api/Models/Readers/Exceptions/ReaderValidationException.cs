//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class ReaderValidationException : Xeption
    {
        public ReaderValidationException(Xeption innerException)
            : base(message: "Reader validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
