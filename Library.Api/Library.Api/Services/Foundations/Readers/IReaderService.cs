//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Readers;

namespace Library.Api.Services.Foundations.Readers
{
    public interface IReaderService
    {
        public ValueTask<Reader> AddReaderAsync(Reader reader);
        public ValueTask<Reader> RetrieveReaderByIdAsync(Guid readerId);
    }
}
