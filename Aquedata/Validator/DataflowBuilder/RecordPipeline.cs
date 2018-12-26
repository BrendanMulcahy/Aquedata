using System;
using System.Threading.Tasks.Dataflow;
using Aquedata.Validator.Parsing.File;
using Aquedata.Validator.Parsing.Record;
using Aquedata.Validator.Sinks;
using Aquedata.Validator.Validation;

namespace Aquedata.Validator.DataflowBuilder
{
    public class RecordPipeline
    {
        public Func<UnparsedRecord, bool> RoutingPredicate { get; }

        // todo maybe not gonna be Validity<ParsedRecord>
        public IPropagatorBlock<string, Validity<ParsedRecord>> Pipeline { get; }

        public ISink<ParsedRecord> ValidRecordSink { get; }
    }
}