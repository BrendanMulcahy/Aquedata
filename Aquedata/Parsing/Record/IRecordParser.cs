using Aquedata.Parsing.File;

namespace Aquedata.Parsing.Record
{
    public interface IRecordParser
    {
        ParsedRecord Parse(UnparsedRecord record);
    }
}