namespace db_nstd
{
    partial class frm_nstd
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbP_result = new System.Windows.Forms.TabPage();
            this.tbP_select = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lv_where = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.txtB_value = new System.Windows.Forms.TextBox();
            this.cmB_condition = new System.Windows.Forms.ComboBox();
            this.cmB_column = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chB_columns = new System.Windows.Forms.CheckedListBox();
            this.tbC_nstd = new System.Windows.Forms.TabControl();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tbP_result.SuspendLayout();
            this.tbP_select.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tbC_nstd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbP_result
            // 
            this.tbP_result.Controls.Add(this.dataGridView1);
            this.tbP_result.Location = new System.Drawing.Point(4, 25);
            this.tbP_result.Name = "tbP_result";
            this.tbP_result.Padding = new System.Windows.Forms.Padding(3);
            this.tbP_result.Size = new System.Drawing.Size(1029, 597);
            this.tbP_result.TabIndex = 3;
            this.tbP_result.Text = "Результат запроса";
            this.tbP_result.UseVisualStyleBackColor = true;
            // 
            // tbP_select
            // 
            this.tbP_select.Controls.Add(this.panel2);
            this.tbP_select.Controls.Add(this.panel1);
            this.tbP_select.Controls.Add(this.chB_columns);
            this.tbP_select.Location = new System.Drawing.Point(4, 25);
            this.tbP_select.Name = "tbP_select";
            this.tbP_select.Padding = new System.Windows.Forms.Padding(3);
            this.tbP_select.Size = new System.Drawing.Size(1029, 597);
            this.tbP_select.TabIndex = 0;
            this.tbP_select.Text = "Выбор полей";
            this.tbP_select.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lv_where);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(279, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(747, 393);
            this.panel2.TabIndex = 2;
            // 
            // lv_where
            // 
            this.lv_where.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_where.HideSelection = false;
            this.lv_where.Location = new System.Drawing.Point(0, 0);
            this.lv_where.Name = "lv_where";
            this.lv_where.Size = new System.Drawing.Size(747, 393);
            this.lv_where.TabIndex = 0;
            this.lv_where.UseCompatibleStateImageBehavior = false;
            this.lv_where.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_delete);
            this.panel1.Controls.Add(this.btn_add);
            this.panel1.Controls.Add(this.txtB_value);
            this.panel1.Controls.Add(this.cmB_condition);
            this.panel1.Controls.Add(this.cmB_column);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(279, 396);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 198);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(557, 31);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(185, 68);
            this.button2.TabIndex = 8;
            this.button2.Text = "Выполнить запрос";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 68);
            this.button1.TabIndex = 1;
            this.button1.Text = "Показать запрос";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(378, 153);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(109, 35);
            this.btn_delete.TabIndex = 7;
            this.btn_delete.Text = "Удалить";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(263, 153);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(109, 35);
            this.btn_add.TabIndex = 6;
            this.btn_add.Text = "Добавить";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // txtB_value
            // 
            this.txtB_value.Location = new System.Drawing.Point(263, 125);
            this.txtB_value.Name = "txtB_value";
            this.txtB_value.Size = new System.Drawing.Size(224, 22);
            this.txtB_value.TabIndex = 5;
            // 
            // cmB_condition
            // 
            this.cmB_condition.FormattingEnabled = true;
            this.cmB_condition.Location = new System.Drawing.Point(263, 78);
            this.cmB_condition.Name = "cmB_condition";
            this.cmB_condition.Size = new System.Drawing.Size(224, 24);
            this.cmB_condition.TabIndex = 4;
            // 
            // cmB_column
            // 
            this.cmB_column.FormattingEnabled = true;
            this.cmB_column.Location = new System.Drawing.Point(263, 31);
            this.cmB_column.Name = "cmB_column";
            this.cmB_column.Size = new System.Drawing.Size(224, 24);
            this.cmB_column.TabIndex = 3;
            this.cmB_column.SelectedIndexChanged += new System.EventHandler(this.cmB_column_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Значение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Условие";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Поле";
            // 
            // chB_columns
            // 
            this.chB_columns.Dock = System.Windows.Forms.DockStyle.Left;
            this.chB_columns.FormattingEnabled = true;
            this.chB_columns.Location = new System.Drawing.Point(3, 3);
            this.chB_columns.Name = "chB_columns";
            this.chB_columns.Size = new System.Drawing.Size(276, 591);
            this.chB_columns.TabIndex = 0;
            // 
            // tbC_nstd
            // 
            this.tbC_nstd.Controls.Add(this.tbP_select);
            this.tbC_nstd.Controls.Add(this.tbP_result);
            this.tbC_nstd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbC_nstd.Location = new System.Drawing.Point(0, 0);
            this.tbC_nstd.Name = "tbC_nstd";
            this.tbC_nstd.SelectedIndex = 0;
            this.tbC_nstd.Size = new System.Drawing.Size(1037, 626);
            this.tbC_nstd.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1023, 591);
            this.dataGridView1.TabIndex = 0;
            // 
            // frm_nstd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 626);
            this.Controls.Add(this.tbC_nstd);
            this.Name = "frm_nstd";
            this.Text = "Нестандартный запрос";
            this.tbP_result.ResumeLayout(false);
            this.tbP_select.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tbC_nstd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tbP_result;
        private System.Windows.Forms.TabPage tbP_select;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckedListBox chB_columns;
        private System.Windows.Forms.TabControl tbC_nstd;
        private System.Windows.Forms.ListView lv_where;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.TextBox txtB_value;
        private System.Windows.Forms.ComboBox cmB_condition;
        private System.Windows.Forms.ComboBox cmB_column;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

