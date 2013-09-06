namespace Pers_uchet_org
{
    partial class StajDohodForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.yearBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.packetcountBox = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.movePacketYearButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.calculateButton = new System.Windows.Forms.Button();
            this.viewButton = new System.Windows.Forms.Button();
            this.movePacketOrgButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.packetView = new System.Windows.Forms.DataGridView();
            this.nppColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datePFColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numPFColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datecreateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateredactColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.doccountBox = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.changeTypedDocButton = new System.Windows.Forms.Button();
            this.printFormButton = new System.Windows.Forms.Button();
            this.moveDocButton = new System.Windows.Forms.Button();
            this.removeDocButton = new System.Windows.Forms.Button();
            this.editDocButton = new System.Windows.Forms.Button();
            this.addDocButton = new System.Windows.Forms.Button();
            this.documentView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.strahnumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeformColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatordocColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datecreatedocColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateredactdocColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetView)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.yearBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(262, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 43);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // yearBox
            // 
            this.yearBox.Location = new System.Drawing.Point(88, 14);
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(97, 20);
            this.yearBox.TabIndex = 1;
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
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.packetcountBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.movePacketYearButton);
            this.groupBox2.Controls.Add(this.printButton);
            this.groupBox2.Controls.Add(this.calculateButton);
            this.groupBox2.Controls.Add(this.viewButton);
            this.groupBox2.Controls.Add(this.movePacketOrgButton);
            this.groupBox2.Controls.Add(this.removeButton);
            this.groupBox2.Controls.Add(this.editButton);
            this.groupBox2.Controls.Add(this.addButton);
            this.groupBox2.Controls.Add(this.packetView);
            this.groupBox2.Location = new System.Drawing.Point(10, 46);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox2.MinimumSize = new System.Drawing.Size(764, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(919, 193);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Пакеты документов СЗВ-1";
            // 
            // packetcountBox
            // 
            this.packetcountBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.packetcountBox.AutoSize = true;
            this.packetcountBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.packetcountBox.Location = new System.Drawing.Point(548, 164);
            this.packetcountBox.Name = "packetcountBox";
            this.packetcountBox.Size = new System.Drawing.Size(14, 13);
            this.packetcountBox.TabIndex = 10;
            this.packetcountBox.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(461, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Всего пакетов";
            // 
            // movePacketYearButton
            // 
            this.movePacketYearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.movePacketYearButton.Location = new System.Drawing.Point(719, 106);
            this.movePacketYearButton.Name = "movePacketYearButton";
            this.movePacketYearButton.Size = new System.Drawing.Size(194, 45);
            this.movePacketYearButton.TabIndex = 8;
            this.movePacketYearButton.Text = "Переместить пакет в гругой отчетный год";
            this.movePacketYearButton.UseVisualStyleBackColor = true;
            this.movePacketYearButton.Click += new System.EventHandler(this.movePacketYearButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Location = new System.Drawing.Point(719, 77);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(194, 23);
            this.printButton.TabIndex = 7;
            this.printButton.Text = "Печать пофамильного списка";
            this.printButton.UseVisualStyleBackColor = true;
            // 
            // calculateButton
            // 
            this.calculateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.calculateButton.Location = new System.Drawing.Point(719, 48);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(194, 23);
            this.calculateButton.TabIndex = 6;
            this.calculateButton.Text = "Калькуляция по пакету...";
            this.calculateButton.UseVisualStyleBackColor = true;
            // 
            // viewButton
            // 
            this.viewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.viewButton.Location = new System.Drawing.Point(719, 19);
            this.viewButton.Name = "viewButton";
            this.viewButton.Size = new System.Drawing.Size(194, 23);
            this.viewButton.TabIndex = 5;
            this.viewButton.Text = "Просмотреть опись в \"СЭВ-2\"...";
            this.viewButton.UseVisualStyleBackColor = true;
            // 
            // movePacketOrgButton
            // 
            this.movePacketOrgButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.movePacketOrgButton.Location = new System.Drawing.Point(625, 128);
            this.movePacketOrgButton.Name = "movePacketOrgButton";
            this.movePacketOrgButton.Size = new System.Drawing.Size(84, 23);
            this.movePacketOrgButton.TabIndex = 4;
            this.movePacketOrgButton.Text = "Переместить в другую организацию";
            this.movePacketOrgButton.UseVisualStyleBackColor = true;
            this.movePacketOrgButton.Click += new System.EventHandler(this.movePacketOrgButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(625, 77);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(88, 23);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Удалить";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.Location = new System.Drawing.Point(625, 48);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(88, 23);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "Изменить";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(625, 19);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(88, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // packetView
            // 
            this.packetView.AllowUserToAddRows = false;
            this.packetView.AllowUserToDeleteRows = false;
            this.packetView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packetView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.packetView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.packetView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.packetView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nppColumn,
            this.datePFColumn,
            this.numPFColumn,
            this.operatorColumn,
            this.datecreateColumn,
            this.dateredactColumn});
            this.packetView.Location = new System.Drawing.Point(6, 19);
            this.packetView.Name = "packetView";
            this.packetView.ReadOnly = true;
            this.packetView.RowHeadersVisible = false;
            this.packetView.Size = new System.Drawing.Size(613, 132);
            this.packetView.TabIndex = 0;
            this.packetView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.packetView_CellDoubleClick);
            // 
            // nppColumn
            // 
            this.nppColumn.HeaderText = "№ п/п";
            this.nppColumn.MaxInputLength = 50;
            this.nppColumn.MinimumWidth = 50;
            this.nppColumn.Name = "nppColumn";
            this.nppColumn.ReadOnly = true;
            this.nppColumn.Width = 50;
            // 
            // datePFColumn
            // 
            this.datePFColumn.HeaderText = "Дата ПФ";
            this.datePFColumn.MaxInputLength = 25;
            this.datePFColumn.MinimumWidth = 80;
            this.datePFColumn.Name = "datePFColumn";
            this.datePFColumn.ReadOnly = true;
            // 
            // numPFColumn
            // 
            this.numPFColumn.HeaderText = "Входящий № ПФ";
            this.numPFColumn.MinimumWidth = 100;
            this.numPFColumn.Name = "numPFColumn";
            this.numPFColumn.ReadOnly = true;
            this.numPFColumn.Width = 120;
            // 
            // operatorColumn
            // 
            this.operatorColumn.HeaderText = "Оператор";
            this.operatorColumn.MinimumWidth = 100;
            this.operatorColumn.Name = "operatorColumn";
            this.operatorColumn.ReadOnly = true;
            this.operatorColumn.Width = 120;
            // 
            // datecreateColumn
            // 
            this.datecreateColumn.HeaderText = "Дата создания";
            this.datecreateColumn.MaxInputLength = 50;
            this.datecreateColumn.MinimumWidth = 80;
            this.datecreateColumn.Name = "datecreateColumn";
            this.datecreateColumn.ReadOnly = true;
            // 
            // dateredactColumn
            // 
            this.dateredactColumn.HeaderText = "Дата редактирования";
            this.dateredactColumn.MaxInputLength = 50;
            this.dateredactColumn.MinimumWidth = 80;
            this.dateredactColumn.Name = "dateredactColumn";
            this.dateredactColumn.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.doccountBox);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.changeTypedDocButton);
            this.groupBox3.Controls.Add(this.printFormButton);
            this.groupBox3.Controls.Add(this.moveDocButton);
            this.groupBox3.Controls.Add(this.removeDocButton);
            this.groupBox3.Controls.Add(this.editDocButton);
            this.groupBox3.Controls.Add(this.addDocButton);
            this.groupBox3.Controls.Add(this.documentView);
            this.groupBox3.Location = new System.Drawing.Point(10, 241);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox3.MinimumSize = new System.Drawing.Size(500, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(920, 278);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Документы СЗВ-1";
            // 
            // doccountBox
            // 
            this.doccountBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.doccountBox.AutoSize = true;
            this.doccountBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.doccountBox.Location = new System.Drawing.Point(746, 254);
            this.doccountBox.Name = "doccountBox";
            this.doccountBox.Size = new System.Drawing.Size(14, 13);
            this.doccountBox.TabIndex = 12;
            this.doccountBox.Text = "0";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(593, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Всего документов в пакете";
            // 
            // changeTypedDocButton
            // 
            this.changeTypedDocButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeTypedDocButton.Location = new System.Drawing.Point(6, 249);
            this.changeTypedDocButton.Name = "changeTypedDocButton";
            this.changeTypedDocButton.Size = new System.Drawing.Size(209, 23);
            this.changeTypedDocButton.TabIndex = 8;
            this.changeTypedDocButton.Text = "Замена типа формы документов";
            this.changeTypedDocButton.UseVisualStyleBackColor = true;
            this.changeTypedDocButton.Click += new System.EventHandler(this.changeTypedDocButton_Click);
            // 
            // printFormButton
            // 
            this.printFormButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.printFormButton.Location = new System.Drawing.Point(221, 249);
            this.printFormButton.Name = "printFormButton";
            this.printFormButton.Size = new System.Drawing.Size(209, 23);
            this.printFormButton.TabIndex = 7;
            this.printFormButton.Text = "Печать форм СЗВ-2, СЗВ-1-1";
            this.printFormButton.UseVisualStyleBackColor = true;
            this.printFormButton.Click += new System.EventHandler(this.printFormButton_Click);
            // 
            // moveDocButton
            // 
            this.moveDocButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveDocButton.Location = new System.Drawing.Point(826, 146);
            this.moveDocButton.Name = "moveDocButton";
            this.moveDocButton.Size = new System.Drawing.Size(88, 23);
            this.moveDocButton.TabIndex = 6;
            this.moveDocButton.Text = "Переместить";
            this.moveDocButton.UseVisualStyleBackColor = true;
            this.moveDocButton.Click += new System.EventHandler(this.moveDocButton_Click);
            // 
            // removeDocButton
            // 
            this.removeDocButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeDocButton.Location = new System.Drawing.Point(826, 77);
            this.removeDocButton.Name = "removeDocButton";
            this.removeDocButton.Size = new System.Drawing.Size(88, 23);
            this.removeDocButton.TabIndex = 5;
            this.removeDocButton.Text = "Удалить";
            this.removeDocButton.UseVisualStyleBackColor = true;
            // 
            // editDocButton
            // 
            this.editDocButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editDocButton.Location = new System.Drawing.Point(826, 48);
            this.editDocButton.Name = "editDocButton";
            this.editDocButton.Size = new System.Drawing.Size(88, 23);
            this.editDocButton.TabIndex = 4;
            this.editDocButton.Text = "Изменить";
            this.editDocButton.UseVisualStyleBackColor = true;
            this.editDocButton.Click += new System.EventHandler(this.editDocButton_Click);
            // 
            // addDocButton
            // 
            this.addDocButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addDocButton.Location = new System.Drawing.Point(826, 19);
            this.addDocButton.Name = "addDocButton";
            this.addDocButton.Size = new System.Drawing.Size(88, 23);
            this.addDocButton.TabIndex = 3;
            this.addDocButton.Text = "Добавить";
            this.addDocButton.UseVisualStyleBackColor = true;
            this.addDocButton.Click += new System.EventHandler(this.addDocButton_Click);
            // 
            // documentView
            // 
            this.documentView.AllowUserToAddRows = false;
            this.documentView.AllowUserToDeleteRows = false;
            this.documentView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.documentView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.documentView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.documentView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.documentView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.documentView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.strahnumColumn,
            this.fioColumn,
            this.typeformColumn,
            this.categoryColumn,
            this.operatordocColumn,
            this.datecreatedocColumn,
            this.dateredactdocColumn});
            this.documentView.Location = new System.Drawing.Point(6, 19);
            this.documentView.Name = "documentView";
            this.documentView.ReadOnly = true;
            this.documentView.RowHeadersWidth = 12;
            this.documentView.Size = new System.Drawing.Size(814, 224);
            this.documentView.TabIndex = 0;
            this.documentView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.documentView_CellDoubleClick);
            // 
            // checkColumn
            // 
            this.checkColumn.HeaderText = "*";
            this.checkColumn.MinimumWidth = 25;
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.ReadOnly = true;
            this.checkColumn.Width = 25;
            // 
            // strahnumColumn
            // 
            this.strahnumColumn.HeaderText = "Страховой №";
            this.strahnumColumn.MaxInputLength = 100;
            this.strahnumColumn.MinimumWidth = 80;
            this.strahnumColumn.Name = "strahnumColumn";
            this.strahnumColumn.ReadOnly = true;
            this.strahnumColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.strahnumColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.strahnumColumn.Width = 80;
            // 
            // fioColumn
            // 
            this.fioColumn.HeaderText = "Фамилия И.О.";
            this.fioColumn.MaxInputLength = 200;
            this.fioColumn.MinimumWidth = 120;
            this.fioColumn.Name = "fioColumn";
            this.fioColumn.ReadOnly = true;
            this.fioColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fioColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fioColumn.Width = 150;
            // 
            // typeformColumn
            // 
            this.typeformColumn.HeaderText = "Тип формы СЗВ";
            this.typeformColumn.MaxInputLength = 200;
            this.typeformColumn.MinimumWidth = 100;
            this.typeformColumn.Name = "typeformColumn";
            this.typeformColumn.ReadOnly = true;
            this.typeformColumn.Width = 120;
            // 
            // categoryColumn
            // 
            this.categoryColumn.HeaderText = "Категория";
            this.categoryColumn.MaxInputLength = 100;
            this.categoryColumn.MinimumWidth = 70;
            this.categoryColumn.Name = "categoryColumn";
            this.categoryColumn.ReadOnly = true;
            this.categoryColumn.Width = 80;
            // 
            // operatordocColumn
            // 
            this.operatordocColumn.HeaderText = "Оператор";
            this.operatordocColumn.MaxInputLength = 100;
            this.operatordocColumn.MinimumWidth = 100;
            this.operatordocColumn.Name = "operatordocColumn";
            this.operatordocColumn.ReadOnly = true;
            // 
            // datecreatedocColumn
            // 
            this.datecreatedocColumn.HeaderText = "Дата ввода";
            this.datecreatedocColumn.MaxInputLength = 50;
            this.datecreatedocColumn.MinimumWidth = 70;
            this.datecreatedocColumn.Name = "datecreatedocColumn";
            this.datecreatedocColumn.ReadOnly = true;
            // 
            // dateredactdocColumn
            // 
            this.dateredactdocColumn.HeaderText = "Дата редактирования";
            this.dateredactdocColumn.MaxInputLength = 50;
            this.dateredactdocColumn.MinimumWidth = 80;
            this.dateredactdocColumn.Name = "dateredactdocColumn";
            this.dateredactdocColumn.ReadOnly = true;
            // 
            // StajDohodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(939, 529);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(800, 567);
            this.Name = "StajDohodForm";
            this.Text = "Стаж и доход";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown yearBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView packetView;
        private System.Windows.Forms.DataGridView documentView;
        private System.Windows.Forms.DataGridViewTextBoxColumn nppColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datePFColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numPFColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datecreateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateredactColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn strahnumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fioColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeformColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatordocColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datecreatedocColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateredactdocColumn;
        private System.Windows.Forms.Button movePacketYearButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Button viewButton;
        private System.Windows.Forms.Button movePacketOrgButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button moveDocButton;
        private System.Windows.Forms.Button removeDocButton;
        private System.Windows.Forms.Button editDocButton;
        private System.Windows.Forms.Button addDocButton;
        private System.Windows.Forms.Label packetcountBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label doccountBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button changeTypedDocButton;
        private System.Windows.Forms.Button printFormButton;
    }
}