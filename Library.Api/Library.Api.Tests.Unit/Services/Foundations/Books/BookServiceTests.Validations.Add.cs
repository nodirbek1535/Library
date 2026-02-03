//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using System;
using System.Collections.Generic;
using System.Text;
using Library.Api.Models.Books;
using Library.Api.Models.Books.Exceptions;

namespace Library.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfBookIsNullAndLogItAsync()
        {
            //given
            Book nullBook = null;
            var nullBookException = new NullBookException();
            
            var expectedBookValidationException =
                new BookValidationException(nullBookException);

            //when
            ValueTask<Book> addBookTask = 
                this.bookService.AddBookAsync(nullBook);

            //then
            await Assert.ThrowsAsync<BookValidationException>(() =>
                addBookTask.AsTask());
        }
    }
}
