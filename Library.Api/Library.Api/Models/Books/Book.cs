//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System.Text.Json.Serialization;
using Library.Api.Models.Readers;

namespace Library.Api.Models.Books
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public Guid? ReaderId { get; set; }
        [JsonIgnore]
        public Reader? Reader { get; set; }
    }
}
