//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class InvalidReaderException : Xeption
    {
        public InvalidReaderException()
            : base(message: "Reader is invalid.")
        { }
    }
}

