//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class NotFoundReaderException : Xeption
    {
        public NotFoundReaderException(Guid readerId)
            : base(message: $"Couldn't find reader with id: {readerId}")
        { }
    }
}
