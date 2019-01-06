using Aquedata.Validator.Parsing.Files;

namespace Aquedata.Validator.Parsing.Record
{
    public interface IRecordParser
    {
        ParsedRecord Parse(UnparsedRecord record);
    }
}