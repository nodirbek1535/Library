//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Books;

namespace Library.Api.Models.Readers
{
    public class Reader
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }    
        public List<Book> Books { get; set; }   
    }
}
