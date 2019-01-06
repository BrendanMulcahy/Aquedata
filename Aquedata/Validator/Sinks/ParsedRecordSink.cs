using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using Aquedata.Extensions;
using Aquedata.Validator.Parsing.Record;

namespace Aquedata.Validator.Sinks
{
    public class ParsedRecordSink : ISink<ParsedRecord>
    {
        private readonly int _batchSize;
        private readonly string _connectionString;
        private readonly string _tableName;

        public ParsedRecordSink(string connectionString, string tableName, int batchSize)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _batchSize = batchSize;
        }

        public void Persist(ParsedRecord[] item)
        {
            DataTable dataTable = item.Select(ToExpandoObject).AsDataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = _batchSize;
                    bulkCopy.DestinationTableName = _tableName;
                    try
                    {
                        bulkCopy.WriteToServer(dataTable);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                    }
                }

                transaction.Commit();
            }
        }

        private ExpandoObject ToExpandoObject(ParsedRecord record)
        {
            var expandoObject = new ExpandoObject();
            var collection = (ICollection<KeyValuePair<string, object>>)expandoObject;
            collection.Add(new KeyValuePair<string, object>("Id", record.Id));

            foreach (var kvp in record.Content)
            {
                collection.Add(kvp);
            }

            return expandoObject;
        }
    }
}