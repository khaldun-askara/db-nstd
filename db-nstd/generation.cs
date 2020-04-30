using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_nstd
{
    // tablefrom это та, у которой прописывается ключ, она ссылается на tableto
    class TabEdge
    {
        string tablefrom, tableto, columnfrom, columnto, path;
        public string Tablefrom { get => tablefrom; set => tablefrom = value; }
        public string Tableto { get => tableto; set => tableto = value; }
        public string Path { get => path; set => path = value; }
        public string Columnfrom { get => columnfrom; set => columnfrom = value; }
        public string Columnto { get => columnto; set => columnto = value; }

        public TabEdge(string tablefrom, string tableto, string columnfrom, string columnto, string path)
        {
            this.tablefrom = tablefrom;
            this.tableto = tableto;
            this.columnfrom = columnfrom;
            this.columnto = columnto;
            this.path = " " + tablefrom + "." + columnfrom + " = " + tableto + "." + columnto + " ";
        }
    }

    class TabGraph
    {
        List<TabEdge> tabEdges;
        internal List<TabEdge> TabEdges1 { get => tabEdges; set => tabEdges = value; }

        public TabGraph (List<TabEdge> tabEdges)
        {
            this.tabEdges = tabEdges;
        }

        public static List<string> GetNeughbors (TabGraph tabGraph, string ver)
        {
            List<string> result = new List<string>();
            foreach (var cur in tabGraph.TabEdges1)
            {
                if (cur.Tablefrom == ver)
                    result.Add(cur.Tableto);
                if (cur.Tableto == ver)
                    result.Add(cur.Tablefrom);
            }
            return result;
        }

        public static string GetPath (TabGraph tabGraph, string ver1, string ver2)
        {
            foreach (var cur in tabGraph.TabEdges1)
                if (cur.Tablefrom == ver1 && cur.Tableto == ver2 || cur.Tableto == ver1 && cur.Tablefrom == ver2)
                    return cur.Path;
            return "";
        }

        public static ISet<string> BNF(string tablefrom, string tableto, TabGraph tabGraph)
        {
            Queue<string> q = new Queue<string>();
            Dictionary<string, bool> used = new Dictionary<string, bool>() { };
            Dictionary<string, string> dst = new Dictionary<string, string>();
            Dictionary<string, string> pr = new Dictionary<string, string>();

            q.Enqueue(tablefrom);
            used[tablefrom] = true;
            dst[tablefrom] = "";
            pr[tablefrom] = "";

            while (q.Count != 0)
            {
                string curr = q.Dequeue();

                foreach (string neighbor in GetNeughbors(tabGraph, curr))
                {
                    if (!used.ContainsKey(neighbor))
                    {
                        q.Enqueue(neighbor);
                        used[neighbor] = true;
                        dst[neighbor] = "";
                        pr[neighbor] = curr;
                    }
                }
            }

            ISet<string> path = new HashSet<string>();
            string cur = tableto;
            path.Add(cur);

            while (pr.ContainsKey(cur) && pr[cur]!= "")
            {
                cur = pr[cur];
                path.Add(cur);
            }

            path.Add(tablefrom);
            path.Reverse();
            return path;
        }
    }

    class generation
    {
        public static string SELECT (CheckedListBox chB_select)
        {
            string result = "SELECT ";
            List<string> selected_columns = new List<string>();
            foreach (db_column item in chB_select.CheckedItems)
                selected_columns.Add(item.Table_name + "." + item.Column_name);
            return result + string.Join(", ", selected_columns);
        }

        public static string FROM (ISet<string> tables)
        {
            string result = " FROM " + string.Join(", ", tables);
            return result;
        }

        public static bool is_in_list (string x, List<string> list)
        {
            foreach (var elem in list)
                if (elem == x) return true;
            return false;
        }

        private static ISet<string> PathToJoins(TabGraph tabGraph, ISet<string> path)
        {
            ISet<string> res = new HashSet<string>();

            using (var iter = path.GetEnumerator())
            {
                string prev = "";
                if (iter.MoveNext())
                {
                    prev = iter.Current;
                }

                while (iter.MoveNext())
                {
                    res.Add(TabGraph.GetPath(tabGraph, prev, iter.Current));
                    prev = iter.Current;
                }
            }

            return res;
        }

        public static List<string> AddMissed (List<string> tables, List<string> max_path)
        {
            foreach (var tb in tables)
                if (!is_in_list(tb, max_path))
                    max_path.Add(tb);
            return max_path;
        }

        public static (string, string) JOIN(List<db_column> selected_columns)
        {
            // список всех таблиц, которые нужно сджоинить
            ISet<string> tables = new HashSet<string>();
            foreach (db_column item in selected_columns)
                tables.Add(item.Table_name);

            // граф с ключами
            TabGraph tabGraph = new TabGraph(database_funcs.GetFKeys());

            ISet<string> total_path = new HashSet<string>();
            ISet<string> extended_tables = new HashSet<string>();
            foreach (var x in tables)
            {
                extended_tables.Add(x);
            }
            foreach (string x in tables)
            {
                foreach (string y in tables)
                {
                    var path = TabGraph.BNF(x, y, tabGraph);
                    var joins = PathToJoins(tabGraph, path);
                    foreach (var z in joins)
                    {
                        total_path.Add(z);
                    }
                    foreach (var z in path)
                    {
                        extended_tables.Add(z);
                    }
                }
            }

            if (total_path.Count > 0)
            {
                return (FROM(extended_tables), "(" + total_path.Aggregate((x, y) => (x + " AND " + y)) + ")");
            } else
            {
                return (FROM(extended_tables) + "\n", "");
            }
        }

        // это просто генерируется код, он не для выполнения, а чтобы показать
        public static string WHERE(ListView lv_where)
        {
            if (lv_where.Items.Count == 0)
                return "";
            List<string> conditions = new List<string>();
            foreach (ListViewItem item in lv_where.Items)
            {
                var columnname = ((db_column)item.Tag).Table_name + "." + ((db_column)item.Tag).Column_name as string;
                var condition = item.SubItems[1].Text as string;
                var value = item.SubItems[2].Text as string;
                conditions.Add(" " + columnname + " " + condition + " " + value + " ");
            }
            return string.Join(" AND ", conditions);
        }
    }
}
