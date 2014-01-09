namespace Pers_uchet_org.Forms
{
    partial class SvodVedomostGetPacketsForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.yearBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.regnumBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.orgnameBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.packetView = new System.Windows.Forms.DataGridView();
            this.check_Column = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.list_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatorEditColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateredactColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.yearBox);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.regnumBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.orgnameBox);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(428, 101);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // yearBox
            // 
            this.yearBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yearBox.Location = new System.Drawing.Point(213, 71);
            this.yearBox.Name = "yearBox";
            this.yearBox.ReadOnly = true;
            this.yearBox.Size = new System.Drawing.Size(209, 20);
            this.yearBox.TabIndex = 23;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(121, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "Отчетный период (год)";
            // 
            // regnumBox
            // 
            this.regnumBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.regnumBox.Location = new System.Drawing.Point(214, 19);
            this.regnumBox.Name = "regnumBox";
            this.regnumBox.ReadOnly = true;
            this.regnumBox.Size = new System.Drawing.Size(209, 20);
            this.regnumBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Геристрационный номер в ПФ";
            // 
            // orgnameBox
            // 
            this.orgnameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.orgnameBox.Location = new System.Drawing.Point(213, 45);
            this.orgnameBox.Name = "orgnameBox";
            this.orgnameBox.ReadOnly = true;
            this.orgnameBox.Size = new System.Drawing.Size(209, 20);
            this.orgnameBox.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Наименование организации";
            // 
            // packetView
            // 
            this.packetView.AllowUserToAddRows = false;
            this.packetView.AllowUserToDeleteRows = false;
            this.packetView.AllowUserToResizeRows = false;
            this.packetView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.packetView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.packetView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.packetView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.packetView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check_Column,
            this.id,
            this.list_type,
            this.operatorEditColumn,
            this.dateredactColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.packetView.DefaultCellStyle = dataGridViewCellStyle2;
            this.packetView.Location = new System.Drawing.Point(12, 119);
            this.packetView.MultiSelect = false;
            this.packetView.Name = "packetView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.packetView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.packetView.RowHeadersVisible = false;
            this.packetView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.packetView.Size = new System.Drawing.Size(428, 250);
            this.packetView.TabIndex = 4;
            // 
            // check_Column
            // 
            this.check_Column.DataPropertyName = "check";
            this.check_Column.HeaderText = "*";
            this.check_Column.Name = "check_Column";
            this.check_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.check_Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.check_Column.Width = 20;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "№";
            this.id.MaxInputLength = 50;
            this.id.MinimumWidth = 50;
            this.id.Name = "id";
            this.id.Width = 50;
            // 
            // list_type
            // 
            this.list_type.DataPropertyName = "name";
            this.list_type.HeaderText = "Тип пакета";
            this.list_type.Name = "list_type";
            this.list_type.Width = 150;
            // 
            // operatorEditColumn
            // 
            this.operatorEditColumn.DataPropertyName = "name_change";
            this.operatorEditColumn.HeaderText = "Оператор";
            this.operatorEditColumn.Name = "operatorEditColumn";
            // 
            // dateredactColumn
            // 
            this.dateredactColumn.DataPropertyName = "change_date";
            this.dateredactColumn.HeaderText = "Дата изменения";
            this.dateredactColumn.MaxInputLength = 50;
            this.dateredactColumn.MinimumWidth = 80;
            this.dateredactColumn.Name = "dateredactColumn";
            this.dateredactColumn.Width = 90;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(275, 375);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(365, 375);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SvodVedomostGetPacketsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 410);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.packetView);
            this.Controls.Add(this.groupBox3);
            this.MinimumSize = new System.Drawing.Size(468, 400);
            this.Name = "SvodVedomostGetPacketsForm";
            this.Text = "Пакеты";
            this.Load += new System.EventHandler(this.SvodVedomostGetPacketsForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SvodVedomostGetPacketsForm_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox yearBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox regnumBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox orgnameBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView packetView;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn list_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatorEditColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateredactColumn;
    }
}