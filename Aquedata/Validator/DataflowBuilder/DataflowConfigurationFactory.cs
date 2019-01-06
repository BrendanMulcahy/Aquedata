using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using Aquedata.Validator.Parsing.Files;
using Aquedata.Validator.Parsing.Record;
using Aquedata.Validator.Sinks;
using Aquedata.Validator.Validation;
using Microsoft.Extensions.Configuration;

namespace Aquedata.Validator.DataflowBuilder
{
    public class DataflowConfigurationFactory : IDataflowConfigurationFactory
    {
        private readonly string _connectionString;

        public DataflowConfigurationFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Aquedata");
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
                            r => new Validity<ParsedRecord>(new ParsedRecord(r.Id,
                                new Dictionary<string, object>
                                {
                                    {"WidgetName", "Foo"},
                                    {"Price", 1.23}
                                }))), // todo real record pipeline
                        new ParsedRecordSink(_connectionString, "ValidRecords", 1000))
                },
                new SqlBulkCopySink<InvalidRecord>(_connectionString, "dbo.InvalidMessages", 1000));
        }
    }
}