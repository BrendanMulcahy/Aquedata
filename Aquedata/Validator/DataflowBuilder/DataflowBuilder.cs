using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Aquedata.Extensions;
using Aquedata.Validator.Parsing.File;
using Aquedata.Validator.Parsing.Record;
using Aquedata.Validator.Validation;

namespace Aquedata.Validator.DataflowBuilder
{
    public class DataflowBuilder : IDataflowBuilder
    {
        // todo this should come from config
        public const int BatchSize = 1000;

        public ITargetBlock<string> Build(DataflowBuilderConfiguration config)
        {
            var sinkCompletions = new List<Task>();

            var fileParser =
                new TransformManyBlock<string, UnparsedRecord>(fileLocation => config.FileParser.Parse(fileLocation));

            var invalidParsedRecordExtractor = new TransformBlock<Validity<ParsedRecord>, InvalidRecord>(validity =>
                new InvalidRecord(validity.Data.Id, validity.InvalidReason));

            ITargetBlock<InvalidRecord> invalidRecordSink =
                DataflowExtensions.CreateBatchedActionBlock<InvalidRecord>(BatchSize, config.InvalidRecordSink.Persist);

            invalidParsedRecordExtractor.LinkTo(invalidRecordSink);
            sinkCompletions.Add(invalidRecordSink.Completion);

            foreach (RecordPipeline recordPipeline in config.RecordPipelines)
            {
                ITargetBlock<Validity<ParsedRecord>> sink = DataflowExtensions.ToTargetPipeline(
                    new TransformBlock<Validity<ParsedRecord>, ParsedRecord>(v => v.Data),
                    DataflowExtensions.CreateBatchedActionBlock<ParsedRecord>(BatchSize,
                        recordPipeline.ValidRecordSink.Persist));

                fileParser.LinkTo(recordPipeline.Pipeline, DataflowExtensions.PropagateCompletion,
                    r => recordPipeline.RoutingPredicate(r));

                recordPipeline.Pipeline.LinkTo(sink, DataflowExtensions.PropagateCompletion, validity => validity.IsValid);
                recordPipeline.Pipeline.LinkTo(invalidParsedRecordExtractor, validity => !validity.IsValid);

                sinkCompletions.Add(sink.Completion);
            }

            // We need to add unroutable last since it will greedily accept all messages.  This won't work if any of
            // the record pipeline have a limited capacity since they could overflow into this link
            var unroutable = new TransformBlock<UnparsedRecord, InvalidRecord>(unparsed =>
                new InvalidRecord(unparsed.Id, "Record did not match any of the pipelines' predicates."));
            fileParser.LinkTo(unroutable, DataflowExtensions.PropagateCompletion);

            invalidParsedRecordExtractor.CompleteWhenAllOrAnyFaulted(config.RecordPipelines.Select(p => p.Pipeline.Completion).ToList());
            invalidRecordSink.CompleteWhenAllOrAnyFaulted(new List<Task>{ invalidParsedRecordExtractor.Completion, unroutable.Completion});

            return DataflowExtensions.EncapsulateTarget(fileParser, Task.WhenAll(sinkCompletions));
        }
    }
}