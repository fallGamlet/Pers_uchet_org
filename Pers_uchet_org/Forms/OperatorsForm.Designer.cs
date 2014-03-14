namespace Pers_uchet_org
{
    partial class OperatorsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.editButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.changePasswordButton = new System.Windows.Forms.Button();
            this.operatorBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.orgView = new System.Windows.Forms.DataGridView();
            this.orgCheckColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.regNumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orgNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accessGroupBox = new System.Windows.Forms.GroupBox();
            this.importAnketaCheckBox = new System.Windows.Forms.CheckBox();
            this.payStajAccessLevelBox = new System.Windows.Forms.ComboBox();
            this.exchangeDataCheckBox = new System.Windows.Forms.CheckBox();
            this.payStajPrintCheckBox = new System.Windows.Forms.CheckBox();
            this.payStajAccessCheckBox = new System.Windows.Forms.CheckBox();
            this.anketaPrintCheckBox = new System.Windows.Forms.CheckBox();
            this.anketaAccessLevelBox = new System.Windows.Forms.ComboBox();
            this.anketaAccessCheckBox = new System.Windows.Forms.CheckBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orgView)).BeginInit();
            this.accessGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.editButton);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Controls.Add(this.removeButton);
            this.groupBox1.Controls.Add(this.changePasswordButton);
            this.groupBox1.Controls.Add(this.operatorBox);
            this.groupBox1.Location = new System.Drawing.Point(9, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Оператор";
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(87, 46);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 3;
            this.editButton.Text = "Изменить";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(6, 46);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(168, 46);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 4;
            this.removeButton.Text = "Удалить";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // changePasswordButton
            // 
            this.changePasswordButton.Location = new System.Drawing.Point(281, 46);
            this.changePasswordButton.Name = "changePasswordButton";
            this.changePasswordButton.Size = new System.Drawing.Size(143, 23);
            this.changePasswordButton.TabIndex = 5;
            this.changePasswordButton.Text = "Сменить пароль...";
            this.changePasswordButton.UseVisualStyleBackColor = true;
            this.changePasswordButton.Click += new System.EventHandler(this.changepasswordButton_Click);
            // 
            // operatorBox
            // 
            this.operatorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operatorBox.FormattingEnabled = true;
            this.operatorBox.Location = new System.Drawing.Point(6, 19);
            this.operatorBox.Name = "operatorBox";
            this.operatorBox.Size = new System.Drawing.Size(418, 21);
            this.operatorBox.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.orgView);
            this.groupBox2.Location = new System.Drawing.Point(9, 78);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 161);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Раздешить работу со следующими организациями";
            // 
            // orgView
            // 
            this.orgView.AllowUserToAddRows = false;
            this.orgView.AllowUserToDeleteRows = false;
            this.orgView.AllowUserToResizeRows = false;
            this.orgView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.orgView.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.orgCheckColumn,
            this.regNumColumn,
            this.orgNameColumn});
            this.orgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgView.Location = new System.Drawing.Point(3, 16);
            this.orgView.MultiSelect = false;
            this.orgView.Name = "orgView";
            this.orgView.RowHeadersVisible = false;
            this.orgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orgView.Size = new System.Drawing.Size(424, 142);
            this.orgView.TabIndex = 0;
            this.orgView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orgView_CellContentClick);
            this.orgView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orgView_CellContentClick);
            this.orgView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.orgView_KeyDown);
            // 
            // org_checkColumn
            // 
            this.orgCheckColumn.DataPropertyName = "check";
            this.orgCheckColumn.HeaderText = "*";
            this.orgCheckColumn.MinimumWidth = 20;
            this.orgCheckColumn.Name = "org_checkColumn";
            this.orgCheckColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.orgCheckColumn.Width = 20;
            // 
            // regnumColumn
            // 
            this.regNumColumn.DataPropertyName = "regnum";
            this.regNumColumn.HeaderText = "Рег. №";
            this.regNumColumn.MaxInputLength = 50;
            this.regNumColumn.MinimumWidth = 50;
            this.regNumColumn.Name = "regnumColumn";
            this.regNumColumn.ReadOnly = true;
            this.regNumColumn.Width = 70;
            // 
            // orgnameColumn
            // 
            this.orgNameColumn.DataPropertyName = "name";
            this.orgNameColumn.HeaderText = "Наименование организации";
            this.orgNameColumn.MaxInputLength = 300;
            this.orgNameColumn.MinimumWidth = 150;
            this.orgNameColumn.Name = "orgnameColumn";
            this.orgNameColumn.ReadOnly = true;
            this.orgNameColumn.Width = 310;
            // 
            // accessGroupBox
            // 
            this.accessGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.accessGroupBox.Controls.Add(this.importAnketaCheckBox);
            this.accessGroupBox.Controls.Add(this.payStajAccessLevelBox);
            this.accessGroupBox.Controls.Add(this.exchangeDataCheckBox);
            this.accessGroupBox.Controls.Add(this.payStajPrintCheckBox);
            this.accessGroupBox.Controls.Add(this.payStajAccessCheckBox);
            this.accessGroupBox.Controls.Add(this.anketaPrintCheckBox);
            this.accessGroupBox.Controls.Add(this.anketaAccessLevelBox);
            this.accessGroupBox.Controls.Add(this.anketaAccessCheckBox);
            this.accessGroupBox.Location = new System.Drawing.Point(9, 239);
            this.accessGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.accessGroupBox.Name = "accessGroupBox";
            this.accessGroupBox.Size = new System.Drawing.Size(430, 188);
            this.accessGroupBox.TabIndex = 2;
            this.accessGroupBox.TabStop = false;
            this.accessGroupBox.Text = "Права доступа";
            // 
            // importAnketaCheckBox
            // 
            this.importAnketaCheckBox.AutoSize = true;
            this.importAnketaCheckBox.Checked = true;
            this.importAnketaCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.importAnketaCheckBox.Location = new System.Drawing.Point(6, 158);
            this.importAnketaCheckBox.Name = "importAnketaCheckBox";
            this.importAnketaCheckBox.Size = new System.Drawing.Size(198, 17);
            this.importAnketaCheckBox.TabIndex = 5;
            this.importAnketaCheckBox.Text = "Импорт анкет из др. организаций";
            this.importAnketaCheckBox.UseVisualStyleBackColor = true;
            // 
            // payStajAccessLevelBox
            // 
            this.payStajAccessLevelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.payStajAccessLevelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.payStajAccessLevelBox.FormattingEnabled = true;
            this.payStajAccessLevelBox.Location = new System.Drawing.Point(294, 76);
            this.payStajAccessLevelBox.Name = "payStajAccessLevelBox";
            this.payStajAccessLevelBox.Size = new System.Drawing.Size(130, 21);
            this.payStajAccessLevelBox.TabIndex = 7;
            // 
            // exchangeDataCheckBox
            // 
            this.exchangeDataCheckBox.AutoSize = true;
            this.exchangeDataCheckBox.Location = new System.Drawing.Point(6, 135);
            this.exchangeDataCheckBox.Name = "exchangeDataCheckBox";
            this.exchangeDataCheckBox.Size = new System.Drawing.Size(251, 17);
            this.exchangeDataCheckBox.TabIndex = 4;
            this.exchangeDataCheckBox.Text = "Обмен электронными данными с ГПФ ПМР";
            this.exchangeDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // payStajPrintCheckBox
            // 
            this.payStajPrintCheckBox.AutoSize = true;
            this.payStajPrintCheckBox.Location = new System.Drawing.Point(6, 101);
            this.payStajPrintCheckBox.Name = "payStajPrintCheckBox";
            this.payStajPrintCheckBox.Size = new System.Drawing.Size(259, 17);
            this.payStajPrintCheckBox.TabIndex = 3;
            this.payStajPrintCheckBox.Text = "Печать сведений о стаже и заработной плате";
            this.payStajPrintCheckBox.UseVisualStyleBackColor = true;
            // 
            // payStajAccessCheckBox
            // 
            this.payStajAccessCheckBox.AutoSize = true;
            this.payStajAccessCheckBox.Location = new System.Drawing.Point(6, 78);
            this.payStajAccessCheckBox.Name = "payStajAccessCheckBox";
            this.payStajAccessCheckBox.Size = new System.Drawing.Size(277, 17);
            this.payStajAccessCheckBox.TabIndex = 2;
            this.payStajAccessCheckBox.Text = "Доступ к сведениям о стаже и заработной плате";
            this.payStajAccessCheckBox.UseVisualStyleBackColor = true;
            this.payStajAccessCheckBox.CheckedChanged += new System.EventHandler(this.paystajaccessCheckBox_CheckedChanged);
            // 
            // anketaPrintCheckBox
            // 
            this.anketaPrintCheckBox.AutoSize = true;
            this.anketaPrintCheckBox.Location = new System.Drawing.Point(6, 44);
            this.anketaPrintCheckBox.Name = "anketaPrintCheckBox";
            this.anketaPrintCheckBox.Size = new System.Drawing.Size(153, 17);
            this.anketaPrintCheckBox.TabIndex = 1;
            this.anketaPrintCheckBox.Text = "Печать анкетных данных";
            this.anketaPrintCheckBox.UseVisualStyleBackColor = true;
            // 
            // anketaAccessLevelBox
            // 
            this.anketaAccessLevelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.anketaAccessLevelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.anketaAccessLevelBox.FormattingEnabled = true;
            this.anketaAccessLevelBox.Location = new System.Drawing.Point(294, 19);
            this.anketaAccessLevelBox.Name = "anketaAccessLevelBox";
            this.anketaAccessLevelBox.Size = new System.Drawing.Size(130, 21);
            this.anketaAccessLevelBox.TabIndex = 6;
            // 
            // anketaAccessCheckBox
            // 
            this.anketaAccessCheckBox.AutoSize = true;
            this.anketaAccessCheckBox.Location = new System.Drawing.Point(6, 21);
            this.anketaAccessCheckBox.Name = "anketaAccessCheckBox";
            this.anketaAccessCheckBox.Size = new System.Drawing.Size(169, 17);
            this.anketaAccessCheckBox.TabIndex = 0;
            this.anketaAccessCheckBox.Text = "Доступ к анкетным данным";
            this.anketaAccessCheckBox.UseVisualStyleBackColor = true;
            this.anketaAccessCheckBox.CheckedChanged += new System.EventHandler(this.anketaaccessCheckBox_CheckedChanged);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(361, 430);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Отмена";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(280, 430);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // OperatorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 465);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.accessGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(464, 500);
            this.Name = "OperatorsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Операторы";
            this.Load += new System.EventHandler(this.OperatorsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.orgView)).EndInit();
            this.accessGroupBox.ResumeLayout(false);
            this.accessGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox accessGroupBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.DataGridView orgView;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button changePasswordButton;
        private System.Windows.Forms.ComboBox operatorBox;
        private System.Windows.Forms.ComboBox anketaAccessLevelBox;
        private System.Windows.Forms.CheckBox anketaAccessCheckBox;
        private System.Windows.Forms.ComboBox payStajAccessLevelBox;
        private System.Windows.Forms.CheckBox exchangeDataCheckBox;
        private System.Windows.Forms.CheckBox payStajPrintCheckBox;
        private System.Windows.Forms.CheckBox payStajAccessCheckBox;
        private System.Windows.Forms.CheckBox anketaPrintCheckBox;
        private System.Windows.Forms.CheckBox importAnketaCheckBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn orgCheckColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regNumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orgNameColumn;
    }
}