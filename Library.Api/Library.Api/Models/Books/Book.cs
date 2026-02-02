//===============================================
//@nodirbek1535 library api program (C)
//===============================================

namespace Library.Api.Models.Books
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public Guid ReaderId { get; set; }
    }
}
