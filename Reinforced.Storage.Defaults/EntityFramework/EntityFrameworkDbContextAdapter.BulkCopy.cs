using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Reinforced.Storage.Adapters;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    public class DontImportToDbAttribute : Attribute
    {

    }

    public partial class EntityFrameworkDbContextAdapter
    {
        private readonly Dictionary<string, SqlConnection> _uploadedTableConnections = new Dictionary<string, SqlConnection>();

        public void UploadBulkTable(Type elementType, IEnumerable source, string toTableName)
        {
            Dictionary<string, int> lengths = null;
            var tmpTable = ConvertToDataTable(elementType, source, toTableName, out lengths);

            if (lengths == null || lengths.Count == 0) return;

            var createTmpTableScript = GetCreateScript(tmpTable, lengths);
            var conn = _dataContext.Database.Connection as SqlConnection;
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

            _uploadedTableConnections[toTableName] = conn;

        }

        public int ExecuteWithTempTable(string tableName, string cmdText, object[] parameters)
        {
            if (!_uploadedTableConnections.ContainsKey(tableName)) 
                throw new Exception($"Cannot find uploaded bulk table {tableName}");
            var conn = _uploadedTableConnections[tableName];

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(new SqlParameter($"@p{i}", parameters[i]));
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public void DropBulkTable(string tableName)
        {
            if (!_uploadedTableConnections.ContainsKey(tableName)) return;
            using (var conn = _uploadedTableConnections[tableName])
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"DROP TABLE {tableName}";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static DataTable ConvertToDataTable(Type elementType, IEnumerable vals, string tableName, out Dictionary<string, int> maxColLengths)
        {
            var props = elementType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.GetCustomAttribute<DontImportToDbAttribute>() == null);

            var table = new DataTable(tableName);
            foreach (var prop in props)
            {
                var col = new DataColumn(
                    prop.Name,
                    Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                col.AllowDBNull = Nullable.GetUnderlyingType(prop.PropertyType) != null;
                table.Columns.Add(col);

            }
            maxColLengths = new Dictionary<string, int>();
            foreach (var val in vals)
            {
                var row = table.NewRow();
                foreach (var prop in props)
                {
                    var v = prop.GetValue(val) ?? DBNull.Value;
                    row[prop.Name] = v;
                    if (prop.PropertyType == typeof(string) && v != DBNull.Value)
                    {
                        var len = v.ToString().Length;
                        if (!maxColLengths.ContainsKey(prop.Name))
                        {
                            maxColLengths[prop.Name] = len;
                        }
                        else
                        {
                            if (len > maxColLengths[prop.Name]) maxColLengths[prop.Name] = len;
                        }

                    }
                }
                table.Rows.Add(row);
            }

            return table;
        }

        private static string GetCreateScript(DataTable schema, Dictionary<string, int> lengths, bool defaultCollate = true)
        {
            var columns = new List<KeyValuePair<string, string>>();
            foreach (DataColumn column in schema.Columns)
            {
                string type;
                var colType = column.DataType;
                bool isNullable = column.AllowDBNull;

                if (colType == typeof(string))
                {
                    int maxLength = lengths[column.ColumnName];

                    type = defaultCollate
                        ? $"NVARCHAR({maxLength}) COLLATE database_default"
                        : $"NVARCHAR({maxLength})";
                }
                else if (colType == typeof(decimal))
                {
                    type = "DECIMAL(18, 2)";
                }
                else if (colType == typeof(int))
                {
                    type = "INT";
                }
                else if (colType == typeof(bool))
                {
                    type = "BIT";
                }
                else if (colType == typeof(float))
                {
                    type = "FLOAT";
                }
                else if (colType == typeof(double))
                {
                    type = "DOUBLE PRECISION";
                }
                else if (colType == typeof(DateTime))
                {
                    type = "DATETIME";
                }
                else if (colType == typeof(long))
                {
                    type = "BIGINT";
                }
                else if (colType.IsEnum)
                {
                    type = "INT";
                }
                else
                {
                    throw new InvalidOperationException($"I do not know SQL type for {column.ColumnName} ({colType})");
                }

                if (isNullable) type = type + " NULL";
                columns.Add(new KeyValuePair<string, string>(column.ColumnName, type));
            }

            var columnNames = String.Join(", ", columns.Select(c => $"[{c.Key}] {c.Value}"));
            return $"CREATE TABLE [{schema.TableName}]({String.Join(", ", columnNames)})";
        }
    }
}
