using System.Collections.Generic;
using Aquedata.Validator.Parsing.Files;
using Aquedata.Validator.Sinks;
using Aquedata.Validator.Validation;

namespace Aquedata.Validator.DataflowBuilder
{
    public class DataflowBuilderConfiguration
    {
        public DataflowBuilderConfiguration(
            IFileParser fileParser,
            List<RecordPipeline> recordPipelines,
            ISink<InvalidRecord> invalidRecordSink)
        {
            FileParser = fileParser;
            RecordPipelines = recordPipelines;
            InvalidRecordSink = invalidRecordSink;
        }

        public IFileParser FileParser { get; }

        public List<RecordPipeline> RecordPipelines { get; }

        public ISink<InvalidRecord> InvalidRecordSink { get; }
    }
}