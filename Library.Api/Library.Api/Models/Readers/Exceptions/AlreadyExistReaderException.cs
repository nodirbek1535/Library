//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class AlreadyExistReaderException : Xeption
    {
        public AlreadyExistReaderException(Exception innerException)
            : base(message: "Reader with the same id already exists.", innerException)
        { }
    }
}
