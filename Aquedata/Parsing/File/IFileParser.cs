using System.Collections.Generic;

namespace Aquedata.Parsing.File
{
    public interface IFileParser
    {
        IEnumerable<UnparsedRecord> Parse(string fileLocation);
    }
}