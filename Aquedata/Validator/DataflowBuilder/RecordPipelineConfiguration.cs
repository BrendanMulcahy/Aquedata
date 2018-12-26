using Aquedata.Validator.Parsing.File;
using Aquedata.Validator.Validation;

namespace Aquedata.Validator.DataflowBuilder
{
    public class RecordPipelineConfiguration
    {
        public IValidator<UnparsedRecord> UnparsedRecordValidator { get; } 
    }
}