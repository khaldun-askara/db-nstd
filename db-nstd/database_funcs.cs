using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NpgsqlTypes;

namespace db_nstd
{
    class db_column
    {
        string column_name;
        string rus_name_column;
        string rus_name_table;
        string table_name;
        string column_type;
        string[] operations;

        public db_column(string column_name, string table_name)
        {
            this.column_name = column_name;
            var temp = database_funcs.GetTranslation(column_name, table_name);
            this.rus_name_column = temp.Item1;
            this.rus_name_table = temp.Item2;
            this.column_type = database_funcs.GetType(column_name, table_name);
            this.operations = GetOperations(column_type);
            this.table_name = table_name;
        }

        public string Column_name { get => column_name; set => column_name = value; }
        public string Rus_name_column { get => rus_name_column; set => rus_name_column = value; }
        public string Table_name { get => table_name; set => table_name = value; }
        public string Column_type { get => column_type; set => column_type = value; }
        public string[] Operations { get => operations; set => operations = value; }
        public string Rus_name_table { get => rus_name_table; set => rus_name_table = value; }

        public static string[] GetOperations(string column_type)
        {
            //if (column_type == "integer")
            //    return new[] { "=", "<>", "<", ">", "<=", ">=" };
            return new[] { "=", "<>" };

        }

        public override string ToString()
        {
            return Rus_name_table + "." + Rus_name_column;
        }
    }
    class database_funcs
    {
        private static readonly string sConnStr = new NpgsqlConnectionStringBuilder
        {
            Host = Database.Default.Host,
            Port = Database.Default.Port,
            Database = Database.Default.Name,
            Username = Environment.GetEnvironmentVariable("POSTGRESQL_USERNAME"),
            Password = Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD"),
            MaxAutoPrepare = 10,
            AutoPrepareMinUsages = 2
        }.ConnectionString;

        public static List<db_column> GetColumnNames()
        {
            using (var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.Connection = sConn;
                    sCommand.CommandText = @"SELECT * FROM helpers.fields ORDER BY table_name, field_name";
                    var reader = sCommand.ExecuteReader();
                    List<db_column> columns = new List<db_column>();
                    while (reader.Read())
                        columns.Add(new db_column((string)reader["field_name"], (string)reader["table_name"]));
                    return columns;
                }
            }
        }

        public static string GetType(string column_name, string table_name)
        {
            using (var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.Connection = sConn;
                    sCommand.CommandText = @"SELECT data_type FROM information_schema.columns
                                            WHERE column_name = @column_name AND table_name = @table_name ";
                    sCommand.Parameters.AddWithValue("@column_name", column_name);
                    sCommand.Parameters.AddWithValue("@table_name", table_name);
                    var reader = sCommand.ExecuteReader();
                    reader.Read();
                    return (string)reader["data_type"];
                }
            }
        }

        public static (string, string) GetTranslation(string column_name, string table_name)
        {
            using (var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.Connection = sConn;
                    sCommand.CommandText = @"SELECT transl_fn, category_name FROM helpers.fields 
                                            WHERE fields.field_name = @field_name AND table_name = @table_name;";
                    sCommand.Parameters.AddWithValue("@field_name", column_name);
                    sCommand.Parameters.AddWithValue("@table_name", table_name);
                    var reader = sCommand.ExecuteReader();
                    if (reader.Read())
                        return ((string)reader["transl_fn"], (string)reader["category_name"]);
                    else return (column_name, table_name);
                }
            }
        }

        public static string GetTableName(string column_name)
        {
            using (var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.Connection = sConn;
                    sCommand.CommandText = @"SELECT table_name FROM information_schema.columns
                                                WHERE column_name = @column_name";
                    sCommand.Parameters.AddWithValue("@column_name", column_name);
                    var reader = sCommand.ExecuteReader();
                    reader.Read();
                    return (string)reader["table_name"];
                }
            }
        }

        public static List<TabEdge> GetFKeys()
        {
            List<TabEdge> tabEdges = new List<TabEdge>();
            using (var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                using (var sCommand = new NpgsqlCommand())
                {
                    List<TabEdge> result = new List<TabEdge>();
                    sCommand.Connection = sConn;
                    sCommand.CommandText = @"SELECT conrelid::regclass AS table_from, pg_get_constraintdef(oid) AS foreign_key
                                    FROM pg_constraint
                                    WHERE contype IN ('f')
                                    AND connamespace = 'public'::regnamespace
                                    ORDER  BY conrelid::regclass::text, contype DESC;";
                    sCommand.AllResultTypesAreUnknown = true;
                    var reader = sCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var tablefrom = (string)reader["table_from"];
                        //FOREIGN KEY (columnfrom) REFERENCES tableto(columnto)
                        string fkey = (string)reader["foreign_key"];
                        //columnfrom) REFERENCES tableto(columnto)
                        string columnfrom = fkey.Substring(fkey.IndexOf('(')+1);
                        //(columnfrom), REFERENCES, tableto(columnto))
                        string[] temp = columnfrom.Split(' ');
                        columnfrom = temp[0].Substring(0, temp[0].Length - 1);
                        string tableto = temp[2].Substring(0, temp[2].IndexOf('(') );
                        string columnto = temp[2].Substring(temp[2].IndexOf('(') + 1);
                        columnto = columnto.Substring(0, columnto.Length - 1);
                        result.Add(new TabEdge(tablefrom, tableto, columnfrom, columnto, " ON " + tablefrom + "." + columnfrom + " = " + tableto + "." + columnto));

                    }
                    return result;
                }
            }
        }
        public static DataTable EXECUTE (ListView lv_where,
            CheckedListBox chB_select, List<db_column> selected_columns)
        {
            if (chB_select.CheckedItems.Count <= 0)
            {
                MessageBox.Show("Вы не  выбрали отрибуты");
                return null;
            }
            using (var sConn = new NpgsqlConnection(sConnStr))
            {
                sConn.Open();
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.Connection = sConn;
                    var select = generation.SELECT(chB_select);
                    var (froms, joins) = generation.JOIN(selected_columns);
                    string res = select + "\n" + froms + "\n";

                    List<string> conditions = new List<string>();
                    foreach (ListViewItem item in lv_where.Items)
                    {
                        var columnname = ((db_column)item.Tag).Table_name + "." + ((db_column)item.Tag).Column_name as string;
                        var condition = item.SubItems[1].Text as string;
                        var value = item.SubItems[2].Text as string;
                        var paramName = "@var" + item.ImageKey;
                        object typedValue = value;
                        if (((db_column)item.Tag).Column_type == "integer"
                            || ((db_column)item.Tag).Column_type == "digint")
                        {
                            int parseResult;
                            if (int.TryParse(value, out parseResult))
                            {
                                typedValue = parseResult;
                            }
                        }
                        else if (((db_column)item.Tag).Column_type == "numeric")
                        {
                            double parseResult;
                            if (double.TryParse(value, out parseResult))
                            {
                                typedValue = parseResult;
                            }
                        }
                        else if (!(((db_column)item.Tag).Column_type == "text" || ((db_column)item.Tag).Column_type == "character varying"))
                        {
                            MessageBox.Show("Тип не поддерживается");
                            continue;
                        }    
                        conditions.Add($"{columnname} {condition} {paramName}");
                        sCommand.Parameters.AddWithValue(paramName, typedValue);
                    }
                    var wheres = string.Join(" AND ", conditions);
                    if (!joins.Equals("") && !wheres.Equals(""))
                    {
                        res += " WHERE " + joins + "AND" + "(" + wheres + ")";
                    }
                    else if (!joins.Equals(""))
                    {
                        res += " WHERE " + joins;
                    }
                    else if (!wheres.Equals(""))
                    {
                        res += " WHERE " + wheres;
                    }
                    sCommand.CommandText += res;
                    var result = new DataTable();
                    result.Load(sCommand.ExecuteReader());
                    return result;
                }
            }
        }
    }
}
