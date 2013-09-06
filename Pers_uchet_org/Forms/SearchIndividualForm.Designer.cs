namespace Pers_uchet_org
{
    partial class SearchIndividualForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.searchButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.categorysearchBox = new System.Windows.Forms.ComboBox();
            this.valuesearchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.orgView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.searchView = new System.Windows.Forms.DataGridView();
            this.departmentView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.orgregnumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orgnameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strahnumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.depnameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orgView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentView)).BeginInit();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(313, 139);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Найти";
            this.searchButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchView);
            this.groupBox1.Location = new System.Drawing.Point(412, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 168);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Результат поиска";
            // 
            // categorysearchBox
            // 
            this.categorysearchBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categorysearchBox.FormattingEnabled = true;
            this.categorysearchBox.Location = new System.Drawing.Point(106, 19);
            this.categorysearchBox.Name = "categorysearchBox";
            this.categorysearchBox.Size = new System.Drawing.Size(282, 21);
            this.categorysearchBox.TabIndex = 2;
            // 
            // valuesearchBox
            // 
            this.valuesearchBox.Location = new System.Drawing.Point(106, 58);
            this.valuesearchBox.Name = "valuesearchBox";
            this.valuesearchBox.Size = new System.Drawing.Size(282, 20);
            this.valuesearchBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Критерий поиска";
            // 
            // orgView
            // 
            this.orgView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.orgView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.orgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.orgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orgregnumColumn,
            this.orgnameColumn});
            this.orgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgView.Location = new System.Drawing.Point(3, 16);
            this.orgView.Name = "orgView";
            this.orgView.ReadOnly = true;
            this.orgView.RowHeadersVisible = false;
            this.orgView.Size = new System.Drawing.Size(388, 177);
            this.orgView.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.orgView);
            this.groupBox2.Location = new System.Drawing.Point(12, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 196);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Список организаций";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.departmentView);
            this.groupBox3.Location = new System.Drawing.Point(412, 186);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(345, 196);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Список структурных подразделений";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.categorysearchBox);
            this.groupBox4.Controls.Add(this.searchButton);
            this.groupBox4.Controls.Add(this.valuesearchBox);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(394, 168);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            // 
            // searchView
            // 
            this.searchView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.searchView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.searchView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.searchView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.strahnumColumn,
            this.fioColumn});
            this.searchView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchView.Location = new System.Drawing.Point(3, 16);
            this.searchView.Name = "searchView";
            this.searchView.ReadOnly = true;
            this.searchView.RowHeadersVisible = false;
            this.searchView.Size = new System.Drawing.Size(339, 149);
            this.searchView.TabIndex = 6;
            // 
            // departmentView
            // 
            this.departmentView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.departmentView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.departmentView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.departmentView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.departmentView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.depnameColumn});
            this.departmentView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.departmentView.Location = new System.Drawing.Point(3, 16);
            this.departmentView.Name = "departmentView";
            this.departmentView.ReadOnly = true;
            this.departmentView.RowHeadersVisible = false;
            this.departmentView.Size = new System.Drawing.Size(339, 177);
            this.departmentView.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Условие поиска";
            // 
            // orgregnumColumn
            // 
            this.orgregnumColumn.HeaderText = "Рег. №";
            this.orgregnumColumn.MaxInputLength = 100;
            this.orgregnumColumn.MinimumWidth = 70;
            this.orgregnumColumn.Name = "orgregnumColumn";
            this.orgregnumColumn.ReadOnly = true;
            // 
            // orgnameColumn
            // 
            this.orgnameColumn.HeaderText = "Наименование организации";
            this.orgnameColumn.MaxInputLength = 300;
            this.orgnameColumn.MinimumWidth = 200;
            this.orgnameColumn.Name = "orgnameColumn";
            this.orgnameColumn.ReadOnly = true;
            this.orgnameColumn.Width = 280;
            // 
            // strahnumColumn
            // 
            this.strahnumColumn.HeaderText = "Страховой №";
            this.strahnumColumn.MaxInputLength = 50;
            this.strahnumColumn.MinimumWidth = 100;
            this.strahnumColumn.Name = "strahnumColumn";
            this.strahnumColumn.ReadOnly = true;
            this.strahnumColumn.Width = 110;
            // 
            // fioColumn
            // 
            this.fioColumn.HeaderText = "ФИО";
            this.fioColumn.MaxInputLength = 200;
            this.fioColumn.MinimumWidth = 200;
            this.fioColumn.Name = "fioColumn";
            this.fioColumn.ReadOnly = true;
            this.fioColumn.Width = 200;
            // 
            // depnameColumn
            // 
            this.depnameColumn.HeaderText = "Наименование отдела (цеха)";
            this.depnameColumn.MaxInputLength = 300;
            this.depnameColumn.MinimumWidth = 300;
            this.depnameColumn.Name = "depnameColumn";
            this.depnameColumn.ReadOnly = true;
            this.depnameColumn.Width = 310;
            // 
            // SearchIndividualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 394);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SearchIndividualForm";
            this.Text = "Поиск физического лица";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.orgView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.departmentView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox categorysearchBox;
        private System.Windows.Forms.TextBox valuesearchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView orgView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView searchView;
        private System.Windows.Forms.DataGridView departmentView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn orgregnumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orgnameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn strahnumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fioColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn depnameColumn;
    }
}