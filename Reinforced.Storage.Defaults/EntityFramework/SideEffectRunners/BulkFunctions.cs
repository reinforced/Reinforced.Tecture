using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Defaults.EntityFramework.SideEffectRunners
{
    class BulkFunctions
    {
        public static DataTable ConvertToDataTable(Type elementType, IEnumerable vals, string tableName, out Dictionary<string, int> maxColLengths)
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

        public static string GetCreateScript(DataTable schema, Dictionary<string, int> lengths, bool defaultCollate = true)
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
