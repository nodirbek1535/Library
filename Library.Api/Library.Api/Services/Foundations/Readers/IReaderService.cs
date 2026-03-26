//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Readers;

namespace Library.Api.Services.Foundations.Readers
{
    public interface IReaderService
    {
        ValueTask<Reader> AddReaderAsync(Reader reader);
        ValueTask<Reader> RetrieveReaderByIdAsync(Guid readerId);
        IQueryable<Reader> RetrieveAllReaders();
        ValueTask<Reader> ModifyReaderAsync(Reader reader);
        ValueTask<Reader> RemoveReaderByIdAsync(Guid readerId);
    }
}
