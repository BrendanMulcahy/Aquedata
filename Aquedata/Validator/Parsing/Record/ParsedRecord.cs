using System.Collections.Generic;

namespace Aquedata.Validator.Parsing.Record
{
    public class ParsedRecord
    {
        public ParsedRecord(int id, Dictionary<string, object> content)
        {
            Id = id;
            Content = content;
        }

        /// <summary>
        ///     The sequential number identifying the record.  In most cases, this corresponds to the line number in the file.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     The actual content of the record split into fields, where the keys are the field names and the values are
        ///     the field's content.
        /// </summary>
        public Dictionary<string, object> Content { get; }
    }
}