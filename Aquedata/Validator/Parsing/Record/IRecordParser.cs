using Aquedata.Validator.Parsing.File;

namespace Aquedata.Validator.Parsing.Record
{
    public interface IRecordParser
    {
        ParsedRecord Parse(UnparsedRecord record);
    }
}