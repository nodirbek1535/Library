//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class NullReaderException:Xeption
    {
        public NullReaderException()
            : base(message: "The reader is null.")
        { }
    }
}
