//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class LockedReaderException:Xeption
    {
        public LockedReaderException(Exception innerException)
            : base(message: "Locked reader error occurred, please try again later.", innerException)
        { }
    }
}
