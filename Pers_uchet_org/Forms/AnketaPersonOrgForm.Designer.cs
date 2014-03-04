namespace Pers_uchet_org
{
    partial class AnketaPersonOrgForm
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
            this.personDataLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.orgView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.regnumorgColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameorgColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orgView)).BeginInit();
            this.SuspendLayout();
            // 
            // personDataLabel
            // 
            this.personDataLabel.AutoSize = true;
            this.personDataLabel.Location = new System.Drawing.Point(7, 9);
            this.personDataLabel.Name = "personDataLabel";
            this.personDataLabel.Size = new System.Drawing.Size(201, 13);
            this.personDataLabel.TabIndex = 0;
            this.personDataLabel.Text = "Страховой № Фамилия Имя Отчество";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.orgView);
            this.groupBox1.Location = new System.Drawing.Point(10, 35);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 189);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Список организаций";
            // 
            // orgView
            // 
            this.orgView.AllowUserToAddRows = false;
            this.orgView.AllowUserToDeleteRows = false;
            this.orgView.AllowUserToResizeRows = false;
            this.orgView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.orgView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.orgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.orgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.regnumorgColumn,
            this.nameorgColumn});
            this.orgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgView.Location = new System.Drawing.Point(3, 16);
            this.orgView.MultiSelect = false;
            this.orgView.Name = "orgView";
            this.orgView.RowHeadersVisible = false;
            this.orgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orgView.Size = new System.Drawing.Size(331, 170);
            this.orgView.TabIndex = 0;
            this.orgView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orgView_CellClick);
            this.orgView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.orgView_KeyDown);
            // 
            // checkColumn
            // 
            this.checkColumn.DataPropertyName = "check";
            this.checkColumn.HeaderText = "*";
            this.checkColumn.MinimumWidth = 15;
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.Width = 20;
            // 
            // regnumorgColumn
            // 
            this.regnumorgColumn.DataPropertyName = "regnum";
            this.regnumorgColumn.HeaderText = "Рег. №";
            this.regnumorgColumn.MaxInputLength = 50;
            this.regnumorgColumn.MinimumWidth = 50;
            this.regnumorgColumn.Name = "regnumorgColumn";
            this.regnumorgColumn.ReadOnly = true;
            this.regnumorgColumn.Width = 80;
            // 
            // nameorgColumn
            // 
            this.nameorgColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameorgColumn.DataPropertyName = "name";
            this.nameorgColumn.HeaderText = "Наименование организации";
            this.nameorgColumn.MaxInputLength = 300;
            this.nameorgColumn.MinimumWidth = 150;
            this.nameorgColumn.Name = "nameorgColumn";
            this.nameorgColumn.ReadOnly = true;
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.Location = new System.Drawing.Point(191, 228);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 2;
            this.acceptButton.Text = "Сохранить";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(272, 228);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AnketaPersonOrgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 263);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.personDataLabel);
            this.Name = "AnketaPersonOrgForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Закрепление работника за организацией";
            this.Load += new System.EventHandler(this.AnketaPersonOrgForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.orgView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label personDataLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.DataGridView orgView;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regnumorgColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameorgColumn;
    }
}