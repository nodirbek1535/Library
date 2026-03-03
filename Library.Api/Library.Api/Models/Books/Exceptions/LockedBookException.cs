//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Xeptions;

namespace Library.Api.Models.Books.Exceptions
{
    public class LockedBookException:Xeption
    {
        public LockedBookException(Exception innerException)
            : base(message: "Locked book record error occurred. Please try again later.",
                  innerException)
        { }
    }
}
