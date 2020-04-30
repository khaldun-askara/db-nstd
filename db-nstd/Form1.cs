using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_nstd
{
    public partial class frm_nstd : Form
    {
        List<db_column> selected_columns;
        public frm_nstd()
        {
            InitializeComponent();
            chB_columns.DataSource = database_funcs.GetColumnNames();
            cmB_column.Items.AddRange(database_funcs.GetColumnNames().ToArray());

            lv_where.Clear();
            lv_where.Columns.Add("Поле"); lv_where.Columns.Add("Условие"); lv_where.Columns.Add("Значение");
            lv_where.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv_where.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        public void UpdateSelectedColumnsList()
        {
            selected_columns = new List<db_column>();
            foreach (db_column col in chB_columns.CheckedItems)
                selected_columns.Add(col);
            foreach (ListViewItem item in lv_where.Items)
            {
                selected_columns.Add((db_column)item.Tag);
            }
        }

        private void cmB_column_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmB_condition.SelectedItem = null;
            cmB_condition.Items.Clear();
            if (cmB_column.SelectedItem == null)
                return;
            cmB_condition.Items.AddRange(((db_column)cmB_column.SelectedItem).Operations);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            bool is_cancel = false;
            double temp3 = 0;
            errorProvider1.SetError(cmB_column, "");
            errorProvider2.SetError(cmB_condition, "");
            errorProvider3.SetError(txtB_value, "");

            if (cmB_column.SelectedItem == null)
            {
                errorProvider1.SetError(cmB_column, "Выберите значение!");
                is_cancel = true;
            }
            if (cmB_condition.SelectedItem == null)
            {
                errorProvider2.SetError(cmB_condition, "Выберите значение!");
                is_cancel = true;
            }
            if (string.IsNullOrWhiteSpace(txtB_value.Text))
            {
                errorProvider3.SetError(txtB_value, "Значение не может быть пустым");
                is_cancel = true;
            }
            if (!is_cancel && ((cmB_column.SelectedItem as db_column).Column_type == "integer"))
            {
                int temp = 0;
                if (!Int32.TryParse(txtB_value.Text, out temp))
                {
                    errorProvider3.SetError(txtB_value, "Введите число");
                    is_cancel = true;
                }
            }
            else if ((cmB_column.SelectedItem as db_column).Column_type == "numeric")
            {
                double temp = 0;
                if (!Double.TryParse(txtB_value.Text, out temp))
                {
                    errorProvider3.SetError(txtB_value, "Введите число");
                    is_cancel = true;
                }
            }
            else if (!is_cancel && (cmB_column.SelectedItem as db_column).Column_type == "bigint")
            {
                long temp = 0;
                if (!Int64.TryParse(txtB_value.Text, out temp))
                {
                    errorProvider3.SetError(txtB_value, "Введите число");
                    is_cancel = true;
                }
            }


            if (is_cancel)
                return;

            var lvi = new ListViewItem(new[] {(cmB_column.SelectedItem as db_column).ToString(),
                                                       (string)cmB_condition.SelectedItem,
                                                        txtB_value.Text});
            lvi.Tag = cmB_column.SelectedItem;
            lv_where.Items.Add(lvi);
            cmB_column.SelectedItem = cmB_condition.SelectedItem = null;
            txtB_value.Text = "";

            lv_where.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv_where.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (lv_where.SelectedItems.Count <= 0)
                return;
            foreach (ListViewItem curr_user in lv_where.SelectedItems)
                lv_where.Items.Remove(curr_user);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chB_columns.CheckedItems.Count == 0)
            {
                MessageBox.Show("Поля не выбраны");
            }
            this.UpdateSelectedColumnsList();
            var select = generation.SELECT(chB_columns);
            var (froms, joins) = generation.JOIN(selected_columns);
            var wheres = generation.WHERE(lv_where);


            string res = select + "\n" + froms;
            if (!joins.Equals("") && !wheres.Equals(""))
            {
                res += " WHERE " + joins + "AND" + "(" + wheres + ")";
            } else if (!joins.Equals(""))
            {
                res += " WHERE " + joins;
            } else if (!wheres.Equals(""))
            {
                res += " WHERE " + wheres;
            }


            frm_show_sql show_Sql = new frm_show_sql(res);
            show_Sql.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (chB_columns.CheckedItems.Count == 0)
            {
                MessageBox.Show("Поля не выбраны");
            }
            this.UpdateSelectedColumnsList();
            dataGridView1.DataSource = database_funcs.EXECUTE(lv_where, chB_columns, selected_columns);
            tbC_nstd.SelectedTab = tbP_result;
        }
    }
}
