namespace Aquedata.Parsing.File
{
    public class UnparsedRecord
    {
        public UnparsedRecord(int id, string content)
        {
            Id = id;
            Content = content;
        }

        /// <summary>
        ///     The sequential number identifying the record.  In most cases, this corresponds to the line number in the file.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     The actual content of the record in an unparsed string form
        /// </summary>
        public string Content { get; }
    }
}