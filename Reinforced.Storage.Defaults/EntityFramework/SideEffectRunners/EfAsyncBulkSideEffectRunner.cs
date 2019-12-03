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
    public class EfAsyncBulkSideEffectRunner : ISideEffectRunner<AsyncBulkSideEffect>
    {
        private readonly DbContext _context;
        public EfAsyncBulkSideEffectRunner(DbContext context)
        {
            _context = context;
        }

        private string GetRandomTableName(AsyncBulkSideEffect effect)
        {
            return $"{effect.ElementType.Name}_{Guid.NewGuid().ToString().Replace('-', '_')}";
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(AsyncBulkSideEffect effect)
        {
            throw new Exception("Can not run async bulk operation sychronously");
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public async Task RunAsync(AsyncBulkSideEffect effect)
        {
            var tName = GetRandomTableName(effect);
            var connection = await UploadBulkTableAsync(effect.ElementType, effect.Data, tName);
            if (connection == null) return;

            await effect.Run(tName, x => ExecuteWithTempTableAsync(connection, x));
            await DropBulkTableAsync(connection, tName);
        }

        public async Task<SqlConnection> UploadBulkTableAsync(Type elementType, IEnumerable source, string toTableName)
        {
            Dictionary<string, int> lengths = null;
            var tmpTable = BulkFunctions.ConvertToDataTable(elementType, source, toTableName, out lengths);

            if (lengths == null || lengths.Count == 0) return null;
            var createTmpTableScript = BulkFunctions.GetCreateScript(tmpTable, lengths);

            var conn = _context.Database.Connection as SqlConnection;
            await conn.OpenAsync();

            // Create tmp table for bulk op
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = createTmpTableScript;
                await cmd.ExecuteNonQueryAsync();
            }

            // Do the fast bulk insert
            using (var loader = new SqlBulkCopy(conn))
            {
                loader.BulkCopyTimeout = 9999999;

                loader.DestinationTableName = tmpTable.TableName;
                await loader.WriteToServerAsync(tmpTable);
            }

            return conn;
        }

        public async Task<int> ExecuteWithTempTableAsync(SqlConnection conn, DirectSqlSideEffect ef)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = ef.Command;
                for (int i = 0; i < ef.Parameters.Length; i++)
                {
                    cmd.Parameters.Add(new SqlParameter($"@p{i}", ef.Parameters[i]));
                }

                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DropBulkTableAsync(SqlConnection conn, string tableName)
        {
            using (conn)
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"DROP TABLE {tableName}";
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


    }
}
