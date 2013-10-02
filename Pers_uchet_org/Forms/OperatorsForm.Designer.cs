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
            this.changepasswordButton = new System.Windows.Forms.Button();
            this.operatorBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.orgView = new System.Windows.Forms.DataGridView();
            this.accessGroupBox = new System.Windows.Forms.GroupBox();
            this.importanketaCheckBox = new System.Windows.Forms.CheckBox();
            this.paystajaccesslevelBox = new System.Windows.Forms.ComboBox();
            this.exchangedataCheckBox = new System.Windows.Forms.CheckBox();
            this.paystajprintCheckBox = new System.Windows.Forms.CheckBox();
            this.paystajaccessCheckBox = new System.Windows.Forms.CheckBox();
            this.anketaprintCheckBox = new System.Windows.Forms.CheckBox();
            this.anketaaccesslevelBox = new System.Windows.Forms.ComboBox();
            this.anketaaccessCheckBox = new System.Windows.Forms.CheckBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.org_checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.regnumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orgnameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.groupBox1.Controls.Add(this.changepasswordButton);
            this.groupBox1.Controls.Add(this.operatorBox);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Оператор";
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(87, 64);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 8;
            this.editButton.Text = "Изменить";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(6, 64);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(168, 64);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 6;
            this.removeButton.Text = "Удалить";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // changepasswordButton
            // 
            this.changepasswordButton.Location = new System.Drawing.Point(281, 64);
            this.changepasswordButton.Name = "changepasswordButton";
            this.changepasswordButton.Size = new System.Drawing.Size(143, 23);
            this.changepasswordButton.TabIndex = 5;
            this.changepasswordButton.Text = "Сменить пароль...";
            this.changepasswordButton.UseVisualStyleBackColor = true;
            this.changepasswordButton.Click += new System.EventHandler(this.changepasswordButton_Click);
            // 
            // operatorBox
            // 
            this.operatorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operatorBox.FormattingEnabled = true;
            this.operatorBox.Location = new System.Drawing.Point(6, 28);
            this.operatorBox.Name = "operatorBox";
            this.operatorBox.Size = new System.Drawing.Size(418, 21);
            this.operatorBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.orgView);
            this.groupBox2.Location = new System.Drawing.Point(9, 112);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 174);
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
            this.org_checkColumn,
            this.regnumColumn,
            this.orgnameColumn});
            this.orgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgView.Location = new System.Drawing.Point(3, 16);
            this.orgView.MultiSelect = false;
            this.orgView.Name = "orgView";
            this.orgView.RowHeadersVisible = false;
            this.orgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orgView.Size = new System.Drawing.Size(424, 155);
            this.orgView.TabIndex = 0;
            this.orgView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orgView_CellContentClick);
            this.orgView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orgView_CellContentClick);
            // 
            // accessGroupBox
            // 
            this.accessGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.accessGroupBox.Controls.Add(this.importanketaCheckBox);
            this.accessGroupBox.Controls.Add(this.paystajaccesslevelBox);
            this.accessGroupBox.Controls.Add(this.exchangedataCheckBox);
            this.accessGroupBox.Controls.Add(this.paystajprintCheckBox);
            this.accessGroupBox.Controls.Add(this.paystajaccessCheckBox);
            this.accessGroupBox.Controls.Add(this.anketaprintCheckBox);
            this.accessGroupBox.Controls.Add(this.anketaaccesslevelBox);
            this.accessGroupBox.Controls.Add(this.anketaaccessCheckBox);
            this.accessGroupBox.Location = new System.Drawing.Point(9, 286);
            this.accessGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.accessGroupBox.Name = "accessGroupBox";
            this.accessGroupBox.Size = new System.Drawing.Size(430, 188);
            this.accessGroupBox.TabIndex = 2;
            this.accessGroupBox.TabStop = false;
            this.accessGroupBox.Text = "Права доступа";
            // 
            // importanketaCheckBox
            // 
            this.importanketaCheckBox.AutoSize = true;
            this.importanketaCheckBox.Checked = true;
            this.importanketaCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.importanketaCheckBox.Location = new System.Drawing.Point(6, 158);
            this.importanketaCheckBox.Name = "importanketaCheckBox";
            this.importanketaCheckBox.Size = new System.Drawing.Size(198, 17);
            this.importanketaCheckBox.TabIndex = 9;
            this.importanketaCheckBox.Text = "Импорт анкет из др. организаций";
            this.importanketaCheckBox.UseVisualStyleBackColor = true;
            // 
            // paystajaccesslevelBox
            // 
            this.paystajaccesslevelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paystajaccesslevelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paystajaccesslevelBox.FormattingEnabled = true;
            this.paystajaccesslevelBox.Location = new System.Drawing.Point(294, 76);
            this.paystajaccesslevelBox.Name = "paystajaccesslevelBox";
            this.paystajaccesslevelBox.Size = new System.Drawing.Size(130, 21);
            this.paystajaccesslevelBox.TabIndex = 7;
            // 
            // exchangedataCheckBox
            // 
            this.exchangedataCheckBox.AutoSize = true;
            this.exchangedataCheckBox.Location = new System.Drawing.Point(6, 135);
            this.exchangedataCheckBox.Name = "exchangedataCheckBox";
            this.exchangedataCheckBox.Size = new System.Drawing.Size(251, 17);
            this.exchangedataCheckBox.TabIndex = 6;
            this.exchangedataCheckBox.Text = "Обмен электронными данными с ГПФ ПМР";
            this.exchangedataCheckBox.UseVisualStyleBackColor = true;
            // 
            // paystajprintCheckBox
            // 
            this.paystajprintCheckBox.AutoSize = true;
            this.paystajprintCheckBox.Location = new System.Drawing.Point(6, 101);
            this.paystajprintCheckBox.Name = "paystajprintCheckBox";
            this.paystajprintCheckBox.Size = new System.Drawing.Size(259, 17);
            this.paystajprintCheckBox.TabIndex = 5;
            this.paystajprintCheckBox.Text = "Печать сведений о стаже и заработной плате";
            this.paystajprintCheckBox.UseVisualStyleBackColor = true;
            // 
            // paystajaccessCheckBox
            // 
            this.paystajaccessCheckBox.AutoSize = true;
            this.paystajaccessCheckBox.Location = new System.Drawing.Point(6, 78);
            this.paystajaccessCheckBox.Name = "paystajaccessCheckBox";
            this.paystajaccessCheckBox.Size = new System.Drawing.Size(277, 17);
            this.paystajaccessCheckBox.TabIndex = 3;
            this.paystajaccessCheckBox.Text = "Доступ к сведениям о стаже и заработной плате";
            this.paystajaccessCheckBox.UseVisualStyleBackColor = true;
            this.paystajaccessCheckBox.CheckedChanged += new System.EventHandler(this.paystajaccessCheckBox_CheckedChanged);
            // 
            // anketaprintCheckBox
            // 
            this.anketaprintCheckBox.AutoSize = true;
            this.anketaprintCheckBox.Location = new System.Drawing.Point(6, 44);
            this.anketaprintCheckBox.Name = "anketaprintCheckBox";
            this.anketaprintCheckBox.Size = new System.Drawing.Size(153, 17);
            this.anketaprintCheckBox.TabIndex = 2;
            this.anketaprintCheckBox.Text = "Печать анкетных данных";
            this.anketaprintCheckBox.UseVisualStyleBackColor = true;
            // 
            // anketaaccesslevelBox
            // 
            this.anketaaccesslevelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.anketaaccesslevelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.anketaaccesslevelBox.FormattingEnabled = true;
            this.anketaaccesslevelBox.Location = new System.Drawing.Point(294, 19);
            this.anketaaccesslevelBox.Name = "anketaaccesslevelBox";
            this.anketaaccesslevelBox.Size = new System.Drawing.Size(130, 21);
            this.anketaaccesslevelBox.TabIndex = 1;
            // 
            // anketaaccessCheckBox
            // 
            this.anketaaccessCheckBox.AutoSize = true;
            this.anketaaccessCheckBox.Location = new System.Drawing.Point(6, 21);
            this.anketaaccessCheckBox.Name = "anketaaccessCheckBox";
            this.anketaaccessCheckBox.Size = new System.Drawing.Size(169, 17);
            this.anketaaccessCheckBox.TabIndex = 0;
            this.anketaaccessCheckBox.Text = "Доступ к анкетным данным";
            this.anketaaccessCheckBox.UseVisualStyleBackColor = true;
            this.anketaaccessCheckBox.CheckedChanged += new System.EventHandler(this.anketaaccessCheckBox_CheckedChanged);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(361, 477);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Закрыть";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(280, 477);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // org_checkColumn
            // 
            this.org_checkColumn.DataPropertyName = "check";
            this.org_checkColumn.HeaderText = "*";
            this.org_checkColumn.MinimumWidth = 20;
            this.org_checkColumn.Name = "org_checkColumn";
            this.org_checkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.org_checkColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.org_checkColumn.Width = 20;
            // 
            // regnumColumn
            // 
            this.regnumColumn.DataPropertyName = "regnum";
            this.regnumColumn.HeaderText = "Рег. №";
            this.regnumColumn.MaxInputLength = 50;
            this.regnumColumn.MinimumWidth = 50;
            this.regnumColumn.Name = "regnumColumn";
            this.regnumColumn.ReadOnly = true;
            this.regnumColumn.Width = 70;
            // 
            // orgnameColumn
            // 
            this.orgnameColumn.DataPropertyName = "name";
            this.orgnameColumn.HeaderText = "Наименование организации";
            this.orgnameColumn.MaxInputLength = 300;
            this.orgnameColumn.MinimumWidth = 150;
            this.orgnameColumn.Name = "orgnameColumn";
            this.orgnameColumn.ReadOnly = true;
            this.orgnameColumn.Width = 310;
            // 
            // OperatorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 512);
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
        private System.Windows.Forms.Button changepasswordButton;
        private System.Windows.Forms.ComboBox operatorBox;
        private System.Windows.Forms.ComboBox anketaaccesslevelBox;
        private System.Windows.Forms.CheckBox anketaaccessCheckBox;
        private System.Windows.Forms.ComboBox paystajaccesslevelBox;
        private System.Windows.Forms.CheckBox exchangedataCheckBox;
        private System.Windows.Forms.CheckBox paystajprintCheckBox;
        private System.Windows.Forms.CheckBox paystajaccessCheckBox;
        private System.Windows.Forms.CheckBox anketaprintCheckBox;
        private System.Windows.Forms.CheckBox importanketaCheckBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn org_checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regnumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orgnameColumn;
    }
}