//===============================================
//@nodirbek1535 library api program (C)
//===============================================


using System.Data;
using Library.Api.Models.Readers;
using Library.Api.Models.Readers.Exceptions;

namespace Library.Api.Services.Foundations.Readers
{
    public partial class ReaderService
    {
        private void ValidateReaderOnAdd(Reader reader)
        {
            ValidateReaderNotNull(reader);

            Validate(
                (Rule: IsInvalid(reader.Id), Parameter: nameof(Reader.Id)),
                (Rule: IsInvalid(reader.FirstName), Parameter: nameof(Reader.FirstName)),
                (Rule: IsInvalid(reader.LastName), Parameter: nameof(Reader.LastName)),
                (Rule: IsInvalid(reader.Age), Parameter: nameof(Reader.Age))
            );
        }

        private void ValidateReaderNotNull(Reader reader)
        {
            if (reader is null)
            {
                throw new NullReaderException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required."
        };

        private static dynamic IsInvalid(int number) => new
        {
            Condition = number <= 0,
            Message = "Number is required and should be greater than zero."
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidReaderException = new InvalidReaderException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if(rule.Condition)
                {
                    invalidReaderException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }
            invalidReaderException.ThrowIfContainsErrors();
        }
    }
}
