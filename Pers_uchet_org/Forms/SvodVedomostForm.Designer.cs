namespace Pers_uchet_org.Forms
{
    partial class SvodVedomostForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SvodVedomostForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.yearBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStripSumsSheet = new System.Windows.Forms.ToolStrip();
            this.addStripButton = new System.Windows.Forms.ToolStripButton();
            this.editStripButton = new System.Windows.Forms.ToolStripButton();
            this.delStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printStripButton = new System.Windows.Forms.ToolStripButton();
            this.mergeView = new System.Windows.Forms.DataGridView();
            this.nppColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetcountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doccountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datecreateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateredactColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsSvod = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSvodMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSvodMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delSvodMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printSvodMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.toolStripSumsSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mergeView)).BeginInit();
            this.cmsSvod.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.yearBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(211, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 43);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // yearBox
            // 
            this.yearBox.Location = new System.Drawing.Point(88, 14);
            this.yearBox.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.yearBox.Minimum = new decimal(new int[] {
            1990,
            0,
            0,
            0});
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(72, 20);
            this.yearBox.TabIndex = 1;
            this.yearBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.yearBox.Value = new decimal(new int[] {
            1990,
            0,
            0,
            0});
            this.yearBox.ValueChanged += new System.EventHandler(this.yearBox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Отчетный год";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.toolStripSumsSheet);
            this.groupBox2.Controls.Add(this.mergeView);
            this.groupBox2.Location = new System.Drawing.Point(10, 46);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox2.MinimumSize = new System.Drawing.Size(467, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 241);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // toolStripSumsSheet
            // 
            this.toolStripSumsSheet.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripSumsSheet.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripSumsSheet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStripButton,
            this.editStripButton,
            this.delStripButton,
            this.toolStripSeparator1,
            this.printStripButton});
            this.toolStripSumsSheet.Location = new System.Drawing.Point(6, 16);
            this.toolStripSumsSheet.Name = "toolStripSumsSheet";
            this.toolStripSumsSheet.Size = new System.Drawing.Size(306, 25);
            this.toolStripSumsSheet.TabIndex = 0;
            this.toolStripSumsSheet.Text = "toolStrip1";
            // 
            // addStripButton
            // 
            this.addStripButton.Image = global::Pers_uchet_org.Properties.Resources.plus2_16;
            this.addStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addStripButton.Name = "addStripButton";
            this.addStripButton.Size = new System.Drawing.Size(79, 22);
            this.addStripButton.Text = "Добавить";
            this.addStripButton.Click += new System.EventHandler(this.addStripButton_Click);
            // 
            // editStripButton
            // 
            this.editStripButton.Image = global::Pers_uchet_org.Properties.Resources.edit_16;
            this.editStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editStripButton.Name = "editStripButton";
            this.editStripButton.Size = new System.Drawing.Size(81, 22);
            this.editStripButton.Text = "Изменить";
            this.editStripButton.Click += new System.EventHandler(this.editStripButton_Click);
            // 
            // delStripButton
            // 
            this.delStripButton.Image = global::Pers_uchet_org.Properties.Resources.delete_16;
            this.delStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delStripButton.Name = "delStripButton";
            this.delStripButton.Size = new System.Drawing.Size(71, 22);
            this.delStripButton.Text = "Удалить";
            this.delStripButton.Click += new System.EventHandler(this.delStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // printStripButton
            // 
            this.printStripButton.Image = global::Pers_uchet_org.Properties.Resources.print_16;
            this.printStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printStripButton.Name = "printStripButton";
            this.printStripButton.Size = new System.Drawing.Size(66, 22);
            this.printStripButton.Text = "Печать";
            this.printStripButton.Click += new System.EventHandler(this.printStripButton_Click);
            // 
            // mergeView
            // 
            this.mergeView.AllowUserToAddRows = false;
            this.mergeView.AllowUserToDeleteRows = false;
            this.mergeView.AllowUserToResizeRows = false;
            this.mergeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mergeView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.mergeView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mergeView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.mergeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mergeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nppColumn,
            this.packetcountColumn,
            this.doccountColumn,
            this.operatorColumn,
            this.datecreateColumn,
            this.dateredactColumn});
            this.mergeView.Location = new System.Drawing.Point(6, 44);
            this.mergeView.MultiSelect = false;
            this.mergeView.Name = "mergeView";
            this.mergeView.ReadOnly = true;
            this.mergeView.RowHeadersVisible = false;
            this.mergeView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mergeView.Size = new System.Drawing.Size(555, 191);
            this.mergeView.TabIndex = 1;
            this.mergeView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mergeView_CellDoubleClick);
            this.mergeView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.mergeView_CellMouseDown);
            this.mergeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mergeView_KeyDown);
            this.mergeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mergeView_MouseClick);
            // 
            // nppColumn
            // 
            this.nppColumn.DataPropertyName = "id";
            this.nppColumn.HeaderText = "№ п/п";
            this.nppColumn.MaxInputLength = 50;
            this.nppColumn.MinimumWidth = 30;
            this.nppColumn.Name = "nppColumn";
            this.nppColumn.ReadOnly = true;
            this.nppColumn.Width = 50;
            // 
            // packetcountColumn
            // 
            this.packetcountColumn.DataPropertyName = "list_count";
            this.packetcountColumn.HeaderText = "Количество пакетов";
            this.packetcountColumn.MaxInputLength = 50;
            this.packetcountColumn.MinimumWidth = 80;
            this.packetcountColumn.Name = "packetcountColumn";
            this.packetcountColumn.ReadOnly = true;
            this.packetcountColumn.Width = 90;
            // 
            // doccountColumn
            // 
            this.doccountColumn.DataPropertyName = "doc_count";
            this.doccountColumn.HeaderText = "Количество документов СЗВ-1";
            this.doccountColumn.MaxInputLength = 50;
            this.doccountColumn.MinimumWidth = 80;
            this.doccountColumn.Name = "doccountColumn";
            this.doccountColumn.ReadOnly = true;
            this.doccountColumn.Width = 90;
            // 
            // operatorColumn
            // 
            this.operatorColumn.DataPropertyName = "operator";
            this.operatorColumn.HeaderText = "Оператор";
            this.operatorColumn.MaxInputLength = 200;
            this.operatorColumn.MinimumWidth = 100;
            this.operatorColumn.Name = "operatorColumn";
            this.operatorColumn.ReadOnly = true;
            this.operatorColumn.Width = 120;
            // 
            // datecreateColumn
            // 
            this.datecreateColumn.DataPropertyName = "new_date";
            this.datecreateColumn.HeaderText = "Дата создания";
            this.datecreateColumn.MaxInputLength = 50;
            this.datecreateColumn.MinimumWidth = 80;
            this.datecreateColumn.Name = "datecreateColumn";
            this.datecreateColumn.ReadOnly = true;
            // 
            // dateredactColumn
            // 
            this.dateredactColumn.DataPropertyName = "edit_date";
            this.dateredactColumn.HeaderText = "Дата редактирования";
            this.dateredactColumn.MaxInputLength = 50;
            this.dateredactColumn.MinimumWidth = 80;
            this.dateredactColumn.Name = "dateredactColumn";
            this.dateredactColumn.ReadOnly = true;
            // 
            // cmsSvod
            // 
            this.cmsSvod.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSvodMenuItem,
            this.editSvodMenuItem,
            this.delSvodMenuItem,
            this.toolStripSeparator2,
            this.printSvodMenuItem});
            this.cmsSvod.Name = "cmsSvod";
            this.cmsSvod.Size = new System.Drawing.Size(153, 120);
            // 
            // addSvodMenuItem
            // 
            this.addSvodMenuItem.Image = global::Pers_uchet_org.Properties.Resources.plus2_16;
            this.addSvodMenuItem.Name = "addSvodMenuItem";
            this.addSvodMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addSvodMenuItem.Text = "Добавить";
            this.addSvodMenuItem.Click += new System.EventHandler(this.addSvodMenuItem_Click);
            // 
            // editSvodMenuItem
            // 
            this.editSvodMenuItem.Image = global::Pers_uchet_org.Properties.Resources.edit_16;
            this.editSvodMenuItem.Name = "editSvodMenuItem";
            this.editSvodMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editSvodMenuItem.Text = "Изменить";
            this.editSvodMenuItem.Click += new System.EventHandler(this.editSvodMenuItem_Click);
            // 
            // delSvodMenuItem
            // 
            this.delSvodMenuItem.Image = global::Pers_uchet_org.Properties.Resources.delete_16;
            this.delSvodMenuItem.Name = "delSvodMenuItem";
            this.delSvodMenuItem.Size = new System.Drawing.Size(152, 22);
            this.delSvodMenuItem.Text = "Удалить";
            this.delSvodMenuItem.Click += new System.EventHandler(this.delSvodMenuItem_Click);
            // 
            // printSvodMenuItem
            // 
            this.printSvodMenuItem.Image = global::Pers_uchet_org.Properties.Resources.print_16;
            this.printSvodMenuItem.Name = "printSvodMenuItem";
            this.printSvodMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printSvodMenuItem.Text = "Печать";
            this.printSvodMenuItem.Click += new System.EventHandler(this.printSvodMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(125, 6);
            // 
            // SvodVedomostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(587, 293);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(503, 284);
            this.Name = "SvodVedomostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сводная ведомость";
            this.Load += new System.EventHandler(this.SvodVedomostForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStripSumsSheet.ResumeLayout(false);
            this.toolStripSumsSheet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mergeView)).EndInit();
            this.cmsSvod.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown yearBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView mergeView;
        private System.Windows.Forms.DataGridViewTextBoxColumn nppColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetcountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn doccountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datecreateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateredactColumn;
        private System.Windows.Forms.ToolStrip toolStripSumsSheet;
        private System.Windows.Forms.ToolStripButton addStripButton;
        private System.Windows.Forms.ToolStripButton editStripButton;
        private System.Windows.Forms.ToolStripButton delStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton printStripButton;
        private System.Windows.Forms.ContextMenuStrip cmsSvod;
        private System.Windows.Forms.ToolStripMenuItem addSvodMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSvodMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delSvodMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem printSvodMenuItem;
    }
}