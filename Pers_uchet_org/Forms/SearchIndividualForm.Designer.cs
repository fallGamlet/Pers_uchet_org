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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.searchButton = new System.Windows.Forms.Button();
            this.PersonGroupBox = new System.Windows.Forms.GroupBox();
            this.personView = new System.Windows.Forms.DataGridView();
            this.strahnumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lnameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.orgView = new System.Windows.Forms.DataGridView();
            this.orgregnumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orgnameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrgGroupBox = new System.Windows.Forms.GroupBox();
            this.ValueGroupBox = new System.Windows.Forms.GroupBox();
            this.mnameBox = new System.Windows.Forms.TextBox();
            this.fnameBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.socnumBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PersonGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orgView)).BeginInit();
            this.OrgGroupBox.SuspendLayout();
            this.ValueGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(154, 139);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 8;
            this.searchButton.Text = "Найти";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // PersonGroupBox
            // 
            this.PersonGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PersonGroupBox.Controls.Add(this.personView);
            this.PersonGroupBox.Location = new System.Drawing.Point(263, 9);
            this.PersonGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.PersonGroupBox.Name = "PersonGroupBox";
            this.PersonGroupBox.Size = new System.Drawing.Size(420, 168);
            this.PersonGroupBox.TabIndex = 2;
            this.PersonGroupBox.TabStop = false;
            this.PersonGroupBox.Text = "Результат поиска";
            // 
            // personView
            // 
            this.personView.AllowUserToAddRows = false;
            this.personView.AllowUserToDeleteRows = false;
            this.personView.AllowUserToResizeRows = false;
            this.personView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.personView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.personView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.personView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.personView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.strahnumColumn,
            this.fioColumn});
            this.personView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.personView.Location = new System.Drawing.Point(3, 16);
            this.personView.Name = "personView";
            this.personView.ReadOnly = true;
            this.personView.RowHeadersVisible = false;
            this.personView.Size = new System.Drawing.Size(414, 149);
            this.personView.TabIndex = 0;
            // 
            // strahnumColumn
            // 
            this.strahnumColumn.DataPropertyName = "soc_number";
            this.strahnumColumn.HeaderText = "Страховой №";
            this.strahnumColumn.MaxInputLength = 50;
            this.strahnumColumn.MinimumWidth = 100;
            this.strahnumColumn.Name = "strahnumColumn";
            this.strahnumColumn.ReadOnly = true;
            // 
            // fioColumn
            // 
            this.fioColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fioColumn.DataPropertyName = "fio";
            this.fioColumn.HeaderText = "ФИО";
            this.fioColumn.MaxInputLength = 200;
            this.fioColumn.MinimumWidth = 200;
            this.fioColumn.Name = "fioColumn";
            this.fioColumn.ReadOnly = true;
            // 
            // lnameBox
            // 
            this.lnameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnameBox.Location = new System.Drawing.Point(86, 54);
            this.lnameBox.Name = "lnameBox";
            this.lnameBox.Size = new System.Drawing.Size(143, 20);
            this.lnameBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Страховой №";
            // 
            // orgView
            // 
            this.orgView.AllowUserToAddRows = false;
            this.orgView.AllowUserToDeleteRows = false;
            this.orgView.AllowUserToResizeRows = false;
            this.orgView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.orgView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.orgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.orgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orgregnumColumn,
            this.orgnameColumn});
            this.orgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgView.Location = new System.Drawing.Point(3, 16);
            this.orgView.Name = "orgView";
            this.orgView.ReadOnly = true;
            this.orgView.RowHeadersVisible = false;
            this.orgView.Size = new System.Drawing.Size(668, 179);
            this.orgView.TabIndex = 0;
            // 
            // orgregnumColumn
            // 
            this.orgregnumColumn.DataPropertyName = "regnum";
            this.orgregnumColumn.HeaderText = "Рег. №";
            this.orgregnumColumn.MaxInputLength = 100;
            this.orgregnumColumn.MinimumWidth = 70;
            this.orgregnumColumn.Name = "orgregnumColumn";
            this.orgregnumColumn.ReadOnly = true;
            // 
            // orgnameColumn
            // 
            this.orgnameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.orgnameColumn.DataPropertyName = "name";
            this.orgnameColumn.HeaderText = "Наименование организации";
            this.orgnameColumn.MaxInputLength = 300;
            this.orgnameColumn.MinimumWidth = 200;
            this.orgnameColumn.Name = "orgnameColumn";
            this.orgnameColumn.ReadOnly = true;
            // 
            // OrgGroupBox
            // 
            this.OrgGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrgGroupBox.Controls.Add(this.orgView);
            this.OrgGroupBox.Location = new System.Drawing.Point(9, 187);
            this.OrgGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.OrgGroupBox.Name = "OrgGroupBox";
            this.OrgGroupBox.Size = new System.Drawing.Size(674, 198);
            this.OrgGroupBox.TabIndex = 3;
            this.OrgGroupBox.TabStop = false;
            this.OrgGroupBox.Text = "Список организаций";
            // 
            // ValueGroupBox
            // 
            this.ValueGroupBox.Controls.Add(this.mnameBox);
            this.ValueGroupBox.Controls.Add(this.fnameBox);
            this.ValueGroupBox.Controls.Add(this.label4);
            this.ValueGroupBox.Controls.Add(this.label3);
            this.ValueGroupBox.Controls.Add(this.socnumBox);
            this.ValueGroupBox.Controls.Add(this.label2);
            this.ValueGroupBox.Controls.Add(this.searchButton);
            this.ValueGroupBox.Controls.Add(this.lnameBox);
            this.ValueGroupBox.Controls.Add(this.label1);
            this.ValueGroupBox.Location = new System.Drawing.Point(9, 9);
            this.ValueGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.ValueGroupBox.Name = "ValueGroupBox";
            this.ValueGroupBox.Size = new System.Drawing.Size(235, 168);
            this.ValueGroupBox.TabIndex = 1;
            this.ValueGroupBox.TabStop = false;
            // 
            // mnameBox
            // 
            this.mnameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mnameBox.Location = new System.Drawing.Point(86, 106);
            this.mnameBox.Name = "mnameBox";
            this.mnameBox.Size = new System.Drawing.Size(143, 20);
            this.mnameBox.TabIndex = 7;
            // 
            // fnameBox
            // 
            this.fnameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fnameBox.Location = new System.Drawing.Point(86, 80);
            this.fnameBox.Name = "fnameBox";
            this.fnameBox.Size = new System.Drawing.Size(143, 20);
            this.fnameBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Отчество";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Имя";
            // 
            // socnumBox
            // 
            this.socnumBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.socnumBox.Location = new System.Drawing.Point(86, 19);
            this.socnumBox.Name = "socnumBox";
            this.socnumBox.Size = new System.Drawing.Size(143, 20);
            this.socnumBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Фамилия";
            // 
            // SearchIndividualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 394);
            this.Controls.Add(this.ValueGroupBox);
            this.Controls.Add(this.OrgGroupBox);
            this.Controls.Add(this.PersonGroupBox);
            this.Name = "SearchIndividualForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск физического лица";
            this.Load += new System.EventHandler(this.SearchIndividualForm_Load);
            this.PersonGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.personView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orgView)).EndInit();
            this.OrgGroupBox.ResumeLayout(false);
            this.ValueGroupBox.ResumeLayout(false);
            this.ValueGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.GroupBox PersonGroupBox;
        private System.Windows.Forms.TextBox lnameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView orgView;
        private System.Windows.Forms.GroupBox OrgGroupBox;
        private System.Windows.Forms.GroupBox ValueGroupBox;
        private System.Windows.Forms.DataGridView personView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn orgregnumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orgnameColumn;
        private System.Windows.Forms.TextBox mnameBox;
        private System.Windows.Forms.TextBox fnameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox socnumBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn strahnumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fioColumn;
    }
}