//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using System.Text.Json.Serialization;
using Library.Api.Models.Books;

namespace Library.Api.Models.Readers
{
    public class Reader
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int Age { get; set; }
        [JsonIgnore]
        public List<Book>? Books { get; set; }
    }
}
