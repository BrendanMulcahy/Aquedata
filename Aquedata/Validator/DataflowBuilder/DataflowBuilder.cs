using System;
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
            TransformManyBlock<string, UnparsedRecord> fileParser =
                new TransformManyBlock<string, UnparsedRecord>(fileLocation => config.FileParser.Parse(fileLocation));

            ITargetBlock<InvalidRecord> invalidRecordSink =
                DataflowExtensions.CreateBatchedActionBlock<InvalidRecord>(BatchSize, config.InvalidRecordSink.Persist);

            foreach (RecordPipeline recordPipeline in config.RecordPipelines)
            {
                var sink = DataflowExtensions.CreateBatchedActionBlock<ParsedRecord>(BatchSize, recordPipeline.ValidRecordSink.Persist);
                var extractData = new TransformBlock<Validity<ParsedRecord>, ParsedRecord>(v => v.Data);
                extractData.LinkTo(sink);

                // todo brendan pickup here
                //recordPipeline.Pipeline.LinkTo(sink, validity => validity.IsValid);
            }




            return new ActionBlock<string>(s => Console.WriteLine(s));
        }
    }
}