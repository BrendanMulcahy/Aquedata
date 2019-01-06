using System;
using System.Data;
using System.Data.SqlClient;
using Aquedata.Extensions;

namespace Aquedata.Validator.Sinks
{
    public class SqlBulkCopySink<T> : ISink<T>
    {
        private readonly int _batchSize;
        private readonly string _connectionString;
        private readonly string _tableName;

        public SqlBulkCopySink(string connectionString, string tableName, int batchSize)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _batchSize = batchSize;
        }

        public void Persist(T[] item)
        {
            DataTable dataTable = item.AsDataTable();

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
    }
}