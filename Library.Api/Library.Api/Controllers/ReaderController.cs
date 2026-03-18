//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;
using Library.Api.Services.Foundations.Readers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderController : RESTFulController
    {
        private readonly IReaderService readerService;

        public ReaderController(IReaderService readerService) =>
            this.readerService = readerService;

        [HttpPost]
        public async ValueTask<ActionResult> PostReaderAsync(Reader reader)
        {
            try
            {
                Reader addedReader = await this.readerService.AddReaderAsync(reader);

                return Created(addedReader);
            }
            catch (ReaderValidationException readerValidationException)
            {
                return BadRequest(readerValidationException.InnerException);
            }
            catch (ReaderDependencyValidationException readerDependencyValidationException)
                when (readerDependencyValidationException.InnerException is AlreadyExistReaderException)
            {
                return Conflict(readerDependencyValidationException.InnerException);
            }
            catch (ReaderDependencyException readerDependencyException)
            {
                return InternalServerError(readerDependencyException.InnerException);
            }
            catch (ReaderServiceException readerServiceException)
            {
                return InternalServerError(readerServiceException.InnerException);
            }
        }

        [HttpGet("{readerId}")]
        public async ValueTask<ActionResult> GetReaderByIdAsync(Guid readerId)
        {
            try
            {
                Reader retrievedReader =
                    await this.readerService.RetrieveReaderByIdAsync(readerId);

                return Ok(retrievedReader);
            }
            catch (ReaderValidationException readerValidationException)
            {
                return BadRequest(readerValidationException.InnerException);
            }
            catch (ReaderDependencyException readerDependencyException)
            {
                return InternalServerError(readerDependencyException.InnerException);
            }
            catch (ReaderServiceException readerServiceException)
            {
                return InternalServerError(readerServiceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Reader>> GetAllReaders()
        {
            try
            {
                IQueryable<Reader> allReaders =
                    this.readerService.RetrieveAllReaders();

                return Ok(allReaders);
            }
            catch (ReaderDependencyException readerDependencyException)
            {
                return InternalServerError(readerDependencyException.InnerException);
            }
            catch (ReaderServiceException readerServiceException)
            {
                return InternalServerError(readerServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Reader>> PutReaderAsync(Reader reader)
        {
            try
            {
                Reader modifiedReader =
                    await this.readerService.ModifyReaderAsync(reader);

                return Ok(modifiedReader);
            }
            catch (ReaderValidationException readerValidationException)
            {
                return BadRequest(readerValidationException.InnerException);
            }
            catch (ReaderDependencyValidationException readerDependencyValidationException)
                when (readerDependencyValidationException.InnerException is LockedReaderException)
            {
                return Locked(readerDependencyValidationException.InnerException);
            }
            catch (ReaderDependencyValidationException readerDependencyValidationException)
            {
                return BadRequest(readerDependencyValidationException.InnerException);
            }
            catch (ReaderDependencyException readerDependencyException)
            {
                return InternalServerError(readerDependencyException.InnerException);
            }
            catch (ReaderServiceException readerServiceException)
            {
                return InternalServerError(readerServiceException.InnerException);
            }
        }
    }
}
