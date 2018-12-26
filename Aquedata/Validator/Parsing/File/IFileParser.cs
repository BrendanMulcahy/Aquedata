using System.Collections.Generic;

namespace Aquedata.Validator.Parsing.File
{
    public interface IFileParser
    {
        IEnumerable<UnparsedRecord> Parse(string fileLocation);
    }
}