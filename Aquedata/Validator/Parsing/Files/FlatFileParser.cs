using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aquedata.Validator.Parsing.Files
{
    public class FlatFileParser : IFileParser
    {
        public IEnumerable<UnparsedRecord> Parse(string fileLocation)
        {
            int id = 0;
            return File.ReadLines(fileLocation).Select(s => new UnparsedRecord(id++, s));
        }
    }
}