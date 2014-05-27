namespace Pers_uchet_org.Forms
{
    partial class ClassifierCategoriesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassifierCategoriesForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.classgroupView = new System.Windows.Forms.DataGridView();
            this.classgroup_id_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.classgoup_name_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.classificatorView = new System.Windows.Forms.DataGridView();
            this.classificator_id_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.classificator_code_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.classificatir_classgroup_id_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.classificator_name_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.commentBox = new System.Windows.Forms.RichTextBox();
            this.classpersonGroupBox = new System.Windows.Forms.GroupBox();
            this.classpercentView = new System.Windows.Forms.DataGridView();
            this.lgotatypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tarifColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodGroupBox = new System.Windows.Forms.GroupBox();
            this.endBox = new System.Windows.Forms.TextBox();
            this.startBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classgroupView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classificatorView)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.classpersonGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classpercentView)).BeginInit();
            this.periodGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.classgroupView);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(780, 132);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Раздел";
            // 
            // classgroupView
            // 
            this.classgroupView.AllowUserToAddRows = false;
            this.classgroupView.AllowUserToDeleteRows = false;
            this.classgroupView.AllowUserToResizeRows = false;
            this.classgroupView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.classgroupView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.classgroupView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.classgroupView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.classgroupView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.classgroupView.ColumnHeadersVisible = false;
            this.classgroupView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.classgroup_id_Column,
            this.classgoup_name_Column});
            this.classgroupView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.classgroupView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classgroupView.Location = new System.Drawing.Point(3, 16);
            this.classgroupView.MultiSelect = false;
            this.classgroupView.Name = "classgroupView";
            this.classgroupView.ReadOnly = true;
            this.classgroupView.RowHeadersVisible = false;
            this.classgroupView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.classgroupView.Size = new System.Drawing.Size(774, 113);
            this.classgroupView.TabIndex = 0;
            // 
            // classgroup_id_Column
            // 
            this.classgroup_id_Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.classgroup_id_Column.DataPropertyName = "id";
            this.classgroup_id_Column.HeaderText = "ID";
            this.classgroup_id_Column.MinimumWidth = 50;
            this.classgroup_id_Column.Name = "classgroup_id_Column";
            this.classgroup_id_Column.ReadOnly = true;
            this.classgroup_id_Column.Visible = false;
            // 
            // classgoup_name_Column
            // 
            this.classgoup_name_Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.classgoup_name_Column.DataPropertyName = "name";
            this.classgoup_name_Column.HeaderText = "Название";
            this.classgoup_name_Column.Name = "classgoup_name_Column";
            this.classgoup_name_Column.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.classificatorView);
            this.groupBox2.Location = new System.Drawing.Point(10, 144);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 160);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Категории";
            // 
            // classificatorView
            // 
            this.classificatorView.AllowUserToAddRows = false;
            this.classificatorView.AllowUserToDeleteRows = false;
            this.classificatorView.AllowUserToResizeRows = false;
            this.classificatorView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.classificatorView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.classificatorView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.classificatorView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.classificatorView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.classificatorView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.classificator_id_Column,
            this.classificator_code_Column,
            this.classificatir_classgroup_id_Column,
            this.classificator_name_Column});
            this.classificatorView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.classificatorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classificatorView.Location = new System.Drawing.Point(3, 16);
            this.classificatorView.MultiSelect = false;
            this.classificatorView.Name = "classificatorView";
            this.classificatorView.ReadOnly = true;
            this.classificatorView.RowHeadersVisible = false;
            this.classificatorView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.classificatorView.Size = new System.Drawing.Size(774, 141);
            this.classificatorView.TabIndex = 0;
            // 
            // classificator_id_Column
            // 
            this.classificator_id_Column.DataPropertyName = "id";
            this.classificator_id_Column.HeaderText = "ID";
            this.classificator_id_Column.Name = "classificator_id_Column";
            this.classificator_id_Column.ReadOnly = true;
            this.classificator_id_Column.Visible = false;
            // 
            // classificator_code_Column
            // 
            this.classificator_code_Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.classificator_code_Column.DataPropertyName = "code";
            this.classificator_code_Column.HeaderText = "Код";
            this.classificator_code_Column.MaxInputLength = 100;
            this.classificator_code_Column.MinimumWidth = 100;
            this.classificator_code_Column.Name = "classificator_code_Column";
            this.classificator_code_Column.ReadOnly = true;
            // 
            // classificatir_classgroup_id_Column
            // 
            this.classificatir_classgroup_id_Column.DataPropertyName = "classgroup_id";
            this.classificatir_classgroup_id_Column.HeaderText = "Группа";
            this.classificatir_classgroup_id_Column.Name = "classificatir_classgroup_id_Column";
            this.classificatir_classgroup_id_Column.ReadOnly = true;
            this.classificatir_classgroup_id_Column.Visible = false;
            // 
            // classificator_name_Column
            // 
            this.classificator_name_Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.classificator_name_Column.DataPropertyName = "name";
            this.classificator_name_Column.HeaderText = "Полное наименование";
            this.classificator_name_Column.MaxInputLength = 300;
            this.classificator_name_Column.MinimumWidth = 300;
            this.classificator_name_Column.Name = "classificator_name_Column";
            this.classificator_name_Column.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.commentBox);
            this.groupBox3.Location = new System.Drawing.Point(10, 306);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(780, 66);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Комментарий";
            // 
            // commentBox
            // 
            this.commentBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commentBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentBox.Location = new System.Drawing.Point(3, 16);
            this.commentBox.Name = "commentBox";
            this.commentBox.Size = new System.Drawing.Size(774, 47);
            this.commentBox.TabIndex = 0;
            this.commentBox.Text = "";
            // 
            // classpersonGroupBox
            // 
            this.classpersonGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.classpersonGroupBox.Controls.Add(this.classpercentView);
            this.classpersonGroupBox.Location = new System.Drawing.Point(10, 374);
            this.classpersonGroupBox.Margin = new System.Windows.Forms.Padding(1);
            this.classpersonGroupBox.Name = "classpersonGroupBox";
            this.classpersonGroupBox.Size = new System.Drawing.Size(472, 145);
            this.classpersonGroupBox.TabIndex = 4;
            this.classpersonGroupBox.TabStop = false;
            this.classpersonGroupBox.Text = "Тариф отчислений в ПФ (согласно ЕСН)";
            // 
            // classpercentView
            // 
            this.classpercentView.AllowUserToAddRows = false;
            this.classpercentView.AllowUserToDeleteRows = false;
            this.classpercentView.AllowUserToResizeRows = false;
            this.classpercentView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.classpercentView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.classpercentView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.classpercentView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.classpercentView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.classpercentView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lgotatypeColumn,
            this.tarifColumn,
            this.startColumn,
            this.endColumn});
            this.classpercentView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.classpercentView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classpercentView.Location = new System.Drawing.Point(3, 16);
            this.classpercentView.MultiSelect = false;
            this.classpercentView.Name = "classpercentView";
            this.classpercentView.ReadOnly = true;
            this.classpercentView.RowHeadersVisible = false;
            this.classpercentView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.classpercentView.Size = new System.Drawing.Size(466, 126);
            this.classpercentView.TabIndex = 0;
            // 
            // lgotatypeColumn
            // 
            this.lgotatypeColumn.DataPropertyName = "privilege_name";
            this.lgotatypeColumn.HeaderText = "Вид льготы";
            this.lgotatypeColumn.MaxInputLength = 200;
            this.lgotatypeColumn.MinimumWidth = 110;
            this.lgotatypeColumn.Name = "lgotatypeColumn";
            this.lgotatypeColumn.ReadOnly = true;
            this.lgotatypeColumn.Width = 110;
            // 
            // tarifColumn
            // 
            this.tarifColumn.DataPropertyName = "value";
            this.tarifColumn.HeaderText = "Значение тарифа, %";
            this.tarifColumn.MinimumWidth = 85;
            this.tarifColumn.Name = "tarifColumn";
            this.tarifColumn.ReadOnly = true;
            this.tarifColumn.Width = 85;
            // 
            // startColumn
            // 
            this.startColumn.DataPropertyName = "date_begin";
            this.startColumn.HeaderText = "Начало действия тарифа";
            this.startColumn.MaxInputLength = 25;
            this.startColumn.MinimumWidth = 120;
            this.startColumn.Name = "startColumn";
            this.startColumn.ReadOnly = true;
            this.startColumn.Width = 125;
            // 
            // endColumn
            // 
            this.endColumn.DataPropertyName = "date_end";
            this.endColumn.HeaderText = "Окончание действия тарифа";
            this.endColumn.MaxInputLength = 25;
            this.endColumn.MinimumWidth = 120;
            this.endColumn.Name = "endColumn";
            this.endColumn.ReadOnly = true;
            this.endColumn.Width = 125;
            // 
            // periodGroupBox
            // 
            this.periodGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.periodGroupBox.Controls.Add(this.endBox);
            this.periodGroupBox.Controls.Add(this.startBox);
            this.periodGroupBox.Controls.Add(this.label2);
            this.periodGroupBox.Controls.Add(this.label1);
            this.periodGroupBox.Location = new System.Drawing.Point(484, 374);
            this.periodGroupBox.Margin = new System.Windows.Forms.Padding(1);
            this.periodGroupBox.Name = "periodGroupBox";
            this.periodGroupBox.Size = new System.Drawing.Size(306, 64);
            this.periodGroupBox.TabIndex = 5;
            this.periodGroupBox.TabStop = false;
            this.periodGroupBox.Text = "Период действия категории";
            this.periodGroupBox.Visible = false;
            // 
            // endBox
            // 
            this.endBox.Location = new System.Drawing.Point(190, 23);
            this.endBox.Name = "endBox";
            this.endBox.Size = new System.Drawing.Size(110, 20);
            this.endBox.TabIndex = 2;
            this.endBox.Text = "действующий";
            // 
            // startBox
            // 
            this.startBox.Location = new System.Drawing.Point(25, 23);
            this.startBox.Name = "startBox";
            this.startBox.Size = new System.Drawing.Size(110, 20);
            this.startBox.TabIndex = 1;
            this.startBox.Text = "01.01.2013";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "по";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "с";
            // 
            // ClassifierCategoriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 529);
            this.Controls.Add(this.periodGroupBox);
            this.Controls.Add(this.classpersonGroupBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(568, 498);
            this.Name = "ClassifierCategoriesForm";
            this.Text = "Классификатор категорий";
            this.Load += new System.EventHandler(this.ClassifierCategoriesForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.classgroupView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.classificatorView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.classpersonGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.classpercentView)).EndInit();
            this.periodGroupBox.ResumeLayout(false);
            this.periodGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox classpersonGroupBox;
        private System.Windows.Forms.DataGridView classificatorView;
        private System.Windows.Forms.DataGridView classpercentView;
        private System.Windows.Forms.RichTextBox commentBox;
        private System.Windows.Forms.GroupBox periodGroupBox;
        private System.Windows.Forms.TextBox endBox;
        private System.Windows.Forms.TextBox startBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView classgroupView;
        private System.Windows.Forms.DataGridViewTextBoxColumn classificator_id_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn classificator_code_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn classificatir_classgroup_id_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn classificator_name_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn lgotatypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tarifColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn classgroup_id_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn classgoup_name_Column;
    }
}