using Aquedata.Validator.Parsing.Files;
using Aquedata.Validator.Validation;

namespace Aquedata.Validator.DataflowBuilder
{
    public class RecordPipelineConfiguration
    {
        public IValidator<UnparsedRecord> UnparsedRecordValidator { get; } 
    }
}