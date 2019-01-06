using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Aquedata.Validator.Parsing.Files;
using Aquedata.Validator.Parsing.Record;
using Aquedata.Validator.Sinks;
using Aquedata.Validator.Validation;

namespace Aquedata.Validator.DataflowBuilder
{
    public class DataflowConfigurationFactory : IDataflowConfigurationFactory
    {
        private readonly string _connectionString;

        public DataflowConfigurationFactory()
        {
            _connectionString = @"Server=.; Database=Aquedata; Integrated Security=SSPI;";
        }

        public DataflowBuilderConfiguration GetConfiguration(string format)
        {
            // todo hard-coded

            return new DataflowBuilderConfiguration(
                new FlatFileParser(),
                new List<RecordPipeline>
                {
                    new RecordPipeline(
                        rec => true,
                        new TransformBlock<UnparsedRecord, Validity<ParsedRecord>>(
                            r =>
                            {
                                var split = r.Content.Split(",");
                                return new Validity<ParsedRecord>(new ParsedRecord(r.Id,
                                    new Dictionary<string, object>
                                    {
                                        {"WidgetName", split.First()},
                                        {"Price", double.Parse(split.Last())}
                                    }));
                            }), // todo real record pipeline
                        new ParsedRecordSink(_connectionString, "ValidRecords", 1000))
                },
                new SqlBulkCopySink<InvalidRecord>(_connectionString, "dbo.InvalidMessages", 1000));
        }
    }
}