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
            this.path = path;
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

        public static List<string> BNF (string tablefrom, string tableto, TabGraph tabGraph)
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

            List<string> path = new List<string>();
            string cur = tableto;
            path.Add(cur);

            while (pr.ContainsKey(cur) && pr[cur]!= "")
            {
                cur = pr[cur];
                path.Add(cur);
            }

            path.Reverse();
            return path;
        }
    }

    class generation
    {
        public static string SELECT (CheckedListBox chB_select)
        {
            if (chB_select.CheckedItems.Count == 0)
                return "";
            string result = "SELECT ";
            List<string> selected_columns = new List<string>();
            foreach (db_column item in chB_select.CheckedItems)
                selected_columns.Add(item.Table_name + "." + item.Column_name);
            return result + string.Join(", ", selected_columns);
        }

        public static string CreateFromString (List<string> path, TabGraph tabGraph)
        {
           

            string result = "FROM " + path[0];
            for (int i = 1; i < path.Count; i++)
                result += " JOIN " + path[i] + TabGraph.GetPath(tabGraph, path[i-1], path[i]);

            return result;
        }

        public static bool is_in_list (string x, List<string> list)
        {
            foreach (var elem in list)
                if (elem == x) return true;
            return false;
        }

        public static List<string> AddMissed (List<string> tables, List<string> max_path)
        {
            foreach (var tb in tables)
                if (!is_in_list(tb, max_path))
                    max_path.Add(tb);
            return max_path;
        }

        public static string JOIN (List<db_column> selected_columns)
        {
            if (selected_columns.Count == 0)
                return "";
            // список всех таблиц, которые нужно сджоинить
            List<string> tables = new List<string>();
            foreach (db_column item in selected_columns)
                tables.Add(item.Table_name);
            tables = tables.Distinct().ToList();

            // граф с ключами
            TabGraph tabGraph = new TabGraph(database_funcs.GetFKeys());

            List<List<string>> paths = new List<List<string>>();

            int max_path_length = 0;
            List<string> max_path = new List<string>();

            foreach (string x in tables)
            {
                foreach (string y in tables)
                {
                    List<string> path = TabGraph.BNF(x, y, tabGraph);
                    if (path.Count > max_path_length)
                    {
                        max_path = path;
                        max_path_length = path.Count;
                    }
                    paths.Add(path);
                }
            }
            max_path = AddMissed(tables, max_path);

            return CreateFromString(max_path, tabGraph);
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
            return "WHERE " + string.Join(" AND ", conditions);
        }
    }
}
