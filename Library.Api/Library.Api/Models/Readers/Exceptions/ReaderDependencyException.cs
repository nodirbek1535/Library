//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class ReaderDependencyException : Xeption
    {
        public ReaderDependencyException(Xeption innerException)
            : base("Reader dependency error  occurred, contact support.", innerException)
        { }
    }
}
