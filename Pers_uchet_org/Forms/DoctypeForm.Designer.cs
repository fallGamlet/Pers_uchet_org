namespace Pers_uchet_org
{
    partial class DoctypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoctypeForm));
            this.idoc_typeView = new System.Windows.Forms.DataGridView();
            this.name_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.idoc_typeView)).BeginInit();
            this.SuspendLayout();
            // 
            // idoc_typeView
            // 
            this.idoc_typeView.AllowUserToAddRows = false;
            this.idoc_typeView.AllowUserToDeleteRows = false;
            this.idoc_typeView.AllowUserToResizeRows = false;
            this.idoc_typeView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.idoc_typeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.idoc_typeView.ColumnHeadersVisible = false;
            this.idoc_typeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name_Column});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.idoc_typeView.DefaultCellStyle = dataGridViewCellStyle1;
            this.idoc_typeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idoc_typeView.Location = new System.Drawing.Point(5, 5);
            this.idoc_typeView.MultiSelect = false;
            this.idoc_typeView.Name = "idoc_typeView";
            this.idoc_typeView.ReadOnly = true;
            this.idoc_typeView.RowHeadersVisible = false;
            this.idoc_typeView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.idoc_typeView.Size = new System.Drawing.Size(347, 327);
            this.idoc_typeView.TabIndex = 0;
            // 
            // name_Column
            // 
            this.name_Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name_Column.DataPropertyName = "name";
            this.name_Column.HeaderText = "Название";
            this.name_Column.MinimumWidth = 200;
            this.name_Column.Name = "name_Column";
            this.name_Column.ReadOnly = true;
            // 
            // DoctypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 337);
            this.Controls.Add(this.idoc_typeView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(320, 250);
            this.Name = "DoctypeForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Типы документов, уд. личность";
            ((System.ComponentModel.ISupportInitialize)(this.idoc_typeView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView idoc_typeView;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_Column;

    }
}