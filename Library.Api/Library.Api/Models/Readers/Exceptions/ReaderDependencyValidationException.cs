//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class ReaderDependencyValidationException : Xeption
    {
        public ReaderDependencyValidationException(Xeption innerException)
            : base(message: "Reader dependency validation error occurred, fix the errors and try again.", innerException)
        { }
    }
}
