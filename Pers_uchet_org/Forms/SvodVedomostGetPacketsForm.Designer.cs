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
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.packetNumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.yearBox);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.regnumBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.orgnameBox);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(424, 101);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // yearBox
            // 
            this.yearBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.yearBox.Location = new System.Drawing.Point(145, 71);
            this.yearBox.Name = "yearBox";
            this.yearBox.ReadOnly = true;
            this.yearBox.Size = new System.Drawing.Size(273, 20);
            this.yearBox.TabIndex = 2;
            this.yearBox.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Отчетный год";
            // 
            // regnumBox
            // 
            this.regnumBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.regnumBox.Location = new System.Drawing.Point(145, 19);
            this.regnumBox.Name = "regnumBox";
            this.regnumBox.ReadOnly = true;
            this.regnumBox.Size = new System.Drawing.Size(273, 20);
            this.regnumBox.TabIndex = 0;
            this.regnumBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Регистрационный номер";
            // 
            // orgnameBox
            // 
            this.orgnameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.orgnameBox.Location = new System.Drawing.Point(145, 45);
            this.orgnameBox.Name = "orgnameBox";
            this.orgnameBox.ReadOnly = true;
            this.orgnameBox.Size = new System.Drawing.Size(273, 20);
            this.orgnameBox.TabIndex = 1;
            this.orgnameBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Наименование";
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
            this.checkColumn,
            this.packetNumColumn,
            this.docCountColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.packetView.DefaultCellStyle = dataGridViewCellStyle2;
            this.packetView.Location = new System.Drawing.Point(12, 107);
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
            this.packetView.Size = new System.Drawing.Size(424, 243);
            this.packetView.TabIndex = 1;
            this.packetView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.packetView_CellClick);
            this.packetView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.packetView_KeyDown);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(280, 356);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "ОК";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(361, 356);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // checkColumn
            // 
            this.checkColumn.HeaderText = "*";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.checkColumn.Width = 20;
            // 
            // packetNumColumn
            // 
            this.packetNumColumn.HeaderText = "№ пакета";
            this.packetNumColumn.MaxInputLength = 50;
            this.packetNumColumn.MinimumWidth = 50;
            this.packetNumColumn.Name = "packetNumColumn";
            this.packetNumColumn.ReadOnly = true;
            this.packetNumColumn.Width = 200;
            // 
            // docCountColumn
            // 
            this.docCountColumn.HeaderText = "Количество документов";
            this.docCountColumn.Name = "docCountColumn";
            this.docCountColumn.ReadOnly = true;
            this.docCountColumn.Width = 200;
            // 
            // SvodVedomostGetPacketsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 391);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.packetView);
            this.Controls.Add(this.groupBox3);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "SvodVedomostGetPacketsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пакеты";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SvodVedomostGetPacketsForm_FormClosing);
            this.Load += new System.EventHandler(this.SvodVedomostGetPacketsForm_Load);
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetNumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn docCountColumn;
    }
}