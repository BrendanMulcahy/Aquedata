using System.Collections.Generic;
using Aquedata.Validator.Parsing.File;
using Aquedata.Validator.Sinks;
using Aquedata.Validator.Validation;

namespace Aquedata.Validator.DataflowBuilder
{
    // todo constructor and finish
    public class DataflowBuilderConfiguration
    {
        public IFileParser FileParser { get; }

        public List<RecordPipeline> RecordPipelines { get; }

        public ISink<InvalidRecord> InvalidRecordSink { get; }
    }
}