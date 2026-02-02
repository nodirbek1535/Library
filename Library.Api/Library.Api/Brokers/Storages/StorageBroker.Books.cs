//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Book> Books { get; set; }
    }
}
