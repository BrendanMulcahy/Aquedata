using System;
using System.Threading.Tasks.Dataflow;
using Aquedata.Validator.Parsing.Files;
using Aquedata.Validator.Parsing.Record;
using Aquedata.Validator.Sinks;
using Aquedata.Validator.Validation;

namespace Aquedata.Validator.DataflowBuilder
{
    public class RecordPipeline
    {
        public RecordPipeline(
            Func<UnparsedRecord, bool> routingPredicate,
            IPropagatorBlock<UnparsedRecord, Validity<ParsedRecord>> pipeline,
            ISink<ParsedRecord> validRecordSink)
        {
            RoutingPredicate = routingPredicate;
            Pipeline = pipeline;
            ValidRecordSink = validRecordSink;
        }

        public Func<UnparsedRecord, bool> RoutingPredicate { get; }

        public IPropagatorBlock<UnparsedRecord, Validity<ParsedRecord>> Pipeline { get; }

        public ISink<ParsedRecord> ValidRecordSink { get; }
    }
}