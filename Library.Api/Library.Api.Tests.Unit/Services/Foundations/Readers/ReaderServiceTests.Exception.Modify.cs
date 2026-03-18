//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

namespace Library.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Reader someReader = CreateRandomReader();
            Guid readerId = someReader.Id;
            SqlException sqlException = GetSqlError();

            var failedReaderStorageException =
                new FailedReaderStorageException(sqlException);

            var expectedReaderDependencyException =
                new ReaderDependencyException(failedReaderStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(readerId))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<Reader> modifyReaderTask =
                this.readerService.ModifyReaderAsync(someReader);

            //then
            await Assert.ThrowsAsync<ReaderDependencyException>(() =>
                modifyReaderTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(readerId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is
                (SameExceptionAs(expectedReaderDependencyException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
