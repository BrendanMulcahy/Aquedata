using System.Collections.Generic;

namespace Aquedata.Validator.Parsing.Files
{
    public interface IFileParser
    {
        IEnumerable<UnparsedRecord> Parse(string fileLocation);
    }
}