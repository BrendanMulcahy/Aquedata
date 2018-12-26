using System.Collections.Generic;

namespace Aquedata.Parsing.Record
{
    public class ParsedRecord
    {
        public ParsedRecord(int id, Dictionary<string, string> content)
        {
            Id = id;
            Content = content;
        }

        /// <summary>
        ///     The sequential number identifying the record.  In most cases, this corresponds to the line number in the file.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     The actual content of the record split into unparsed fields, where the keys are the field names and the values are
        ///     the field's unparsed content.
        /// </summary>
        public Dictionary<string, string> Content { get; }
    }
}