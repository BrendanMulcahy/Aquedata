using System;
using System.Data;
using System.Data.SqlClient;
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
            // this is for demo short-cutting only
            DataTable dataTable = item.Select(i => new TableSchema
            {
                Id = i.Id,
                WidgetName = i.Content["WidgetName"],
                Price = i.Content["Price"]
            }).AsDataTable();

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

        private class TableSchema
        {
            public int Id { get; set; }
            public object WidgetName { get; set; }
            public object Price { get; set; }
        }
    }
}