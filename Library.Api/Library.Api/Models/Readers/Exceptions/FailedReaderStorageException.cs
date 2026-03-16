//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Readers.Exceptions
{
    public class FailedReaderStorageException:Xeption
    {
        public FailedReaderStorageException(Exception innerException)
            :base("Failed reader storage error occured, contact support.",innerException)
        { }
    }
}
