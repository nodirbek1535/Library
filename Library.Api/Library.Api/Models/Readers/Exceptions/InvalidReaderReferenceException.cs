//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Xeptions;

public class InvalidReaderReferenceException : Xeption
{
    public InvalidReaderReferenceException(Exception innerException)
        : base("Reader has active books and cannot be deleted.", innerException)
    { }
}