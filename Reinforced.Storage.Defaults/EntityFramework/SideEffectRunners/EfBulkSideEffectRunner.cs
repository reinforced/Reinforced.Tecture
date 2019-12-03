using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Defaults.EntityFramework.SideEffectRunners
{
    public class EfBulkSideEffectRunner : ISideEffectRunner<BulkSideEffect>
    {
        private readonly DbContext _context;
        public EfBulkSideEffectRunner(DbContext context)
        {
            _context = context;
        }

        private string GetRandomTableName(BulkSideEffect effect)
        {
            return $"{effect.ElementType.Name}_{Guid.NewGuid().ToString().Replace('-', '_')}";
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(BulkSideEffect effect)
        {
            var tName = GetRandomTableName(effect);
            var connection = UploadBulkTable(effect.ElementType, effect.Data, tName);
            if (connection==null) return;
            
            effect.Run(tName, x => ExecuteWithTempTable(connection, x));
            DropBulkTable(connection, tName);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(BulkSideEffect effect)
        {
            Run(effect);
            return Task.Delay(0);
        }

        public SqlConnection UploadBulkTable(Type elementType, IEnumerable source, string toTableName)
        {
            Dictionary<string, int> lengths = null;
            var tmpTable = BulkFunctions.ConvertToDataTable(elementType, source, toTableName, out lengths);

            if (lengths == null || lengths.Count == 0) return null;

            var createTmpTableScript = BulkFunctions.GetCreateScript(tmpTable, lengths);
            var conn = _context.Database.Connection as SqlConnection;
            conn.Open();

            // Create tmp table for bulk op
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = createTmpTableScript;
                cmd.ExecuteNonQuery();
            }

            // Do the fast bulk insert
            using (var loader = new SqlBulkCopy(conn))
            {
                loader.BulkCopyTimeout = 9999999;

                loader.DestinationTableName = tmpTable.TableName;
                loader.WriteToServer(tmpTable);
            }

            return conn;

        }

        public int ExecuteWithTempTable(SqlConnection conn, DirectSqlSideEffect ef)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = ef.Command;
                for (int i = 0; i < ef.Parameters.Length; i++)
                {
                    cmd.Parameters.Add(new SqlParameter($"@p{i}", ef.Parameters[i]));
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public void DropBulkTable(SqlConnection conn, string tableName)
        {
            using (conn)
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"DROP TABLE {tableName}";
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
