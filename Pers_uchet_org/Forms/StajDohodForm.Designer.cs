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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.yearBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.packetcountBox = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.movePacketYearButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.calculateButton = new System.Windows.Forms.Button();
            this.reestrButton = new System.Windows.Forms.Button();
            this.movePacketOrgButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.listsView = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.list_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatorRegColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datecreateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatorEditColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateredactColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.docCountBox = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.changeTypedDocButton = new System.Windows.Forms.Button();
            this.printFormButton = new System.Windows.Forms.Button();
            this.moveDocButton = new System.Windows.Forms.Button();
            this.removeDocButton = new System.Windows.Forms.Button();
            this.editDocButton = new System.Windows.Forms.Button();
            this.addDocButton = new System.Windows.Forms.Button();
            this.docView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.socNumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeformColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operRegColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operChangeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changeDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listsView)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.yearBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 43);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // yearBox
            // 
            this.yearBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.yearBox.Location = new System.Drawing.Point(88, 14);
            this.yearBox.Maximum = new decimal(new int[] {
            1970,
            0,
            0,
            0});
            this.yearBox.Minimum = new decimal(new int[] {
            1970,
            0,
            0,
            0});
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(72, 20);
            this.yearBox.TabIndex = 1;
            this.yearBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.yearBox.Value = new decimal(new int[] {
            1970,
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
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.packetcountBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.movePacketYearButton);
            this.groupBox2.Controls.Add(this.printButton);
            this.groupBox2.Controls.Add(this.calculateButton);
            this.groupBox2.Controls.Add(this.reestrButton);
            this.groupBox2.Controls.Add(this.movePacketOrgButton);
            this.groupBox2.Controls.Add(this.removeButton);
            this.groupBox2.Controls.Add(this.addButton);
            this.groupBox2.Controls.Add(this.listsView);
            this.groupBox2.Location = new System.Drawing.Point(10, 46);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(913, 177);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Пакеты документов СЗВ-1";
            // 
            // packetcountBox
            // 
            this.packetcountBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.packetcountBox.Location = new System.Drawing.Point(585, 161);
            this.packetcountBox.Name = "packetcountBox";
            this.packetcountBox.Size = new System.Drawing.Size(28, 13);
            this.packetcountBox.TabIndex = 10;
            this.packetcountBox.Text = "0";
            this.packetcountBox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(495, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Всего пакетов:";
            // 
            // movePacketYearButton
            // 
            this.movePacketYearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.movePacketYearButton.Location = new System.Drawing.Point(713, 106);
            this.movePacketYearButton.Name = "movePacketYearButton";
            this.movePacketYearButton.Size = new System.Drawing.Size(194, 23);
            this.movePacketYearButton.TabIndex = 8;
            this.movePacketYearButton.Text = "Переместить в другой год...";
            this.movePacketYearButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.movePacketYearButton.UseVisualStyleBackColor = true;
            this.movePacketYearButton.Click += new System.EventHandler(this.movePacketYearButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Location = new System.Drawing.Point(713, 77);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(194, 23);
            this.printButton.TabIndex = 7;
            this.printButton.Text = "Печать пофамильного списка...";
            this.printButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.printButton.UseVisualStyleBackColor = true;
            // 
            // calculateButton
            // 
            this.calculateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.calculateButton.Location = new System.Drawing.Point(713, 48);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(194, 23);
            this.calculateButton.TabIndex = 6;
            this.calculateButton.Text = "Калькуляция по пакету...";
            this.calculateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.calculateButton.UseVisualStyleBackColor = true;
            // 
            // reestrButton
            // 
            this.reestrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reestrButton.Location = new System.Drawing.Point(713, 19);
            this.reestrButton.Name = "reestrButton";
            this.reestrButton.Size = new System.Drawing.Size(194, 23);
            this.reestrButton.TabIndex = 5;
            this.reestrButton.Text = "Просмотреть опись \"СЗВ-2\"...";
            this.reestrButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.reestrButton.UseVisualStyleBackColor = true;
            // 
            // movePacketOrgButton
            // 
            this.movePacketOrgButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.movePacketOrgButton.Location = new System.Drawing.Point(713, 135);
            this.movePacketOrgButton.Name = "movePacketOrgButton";
            this.movePacketOrgButton.Size = new System.Drawing.Size(194, 23);
            this.movePacketOrgButton.TabIndex = 4;
            this.movePacketOrgButton.Text = "Переместить в организацию...";
            this.movePacketOrgButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.movePacketOrgButton.UseVisualStyleBackColor = true;
            this.movePacketOrgButton.Click += new System.EventHandler(this.movePacketOrgButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(619, 48);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(88, 23);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Удалить";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(619, 19);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(88, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // listsView
            // 
            this.listsView.AllowUserToAddRows = false;
            this.listsView.AllowUserToDeleteRows = false;
            this.listsView.AllowUserToResizeRows = false;
            this.listsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listsView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.listsView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.listsView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.listsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.list_type,
            this.operatorRegColumn,
            this.datecreateColumn,
            this.operatorEditColumn,
            this.dateredactColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listsView.DefaultCellStyle = dataGridViewCellStyle2;
            this.listsView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.listsView.Location = new System.Drawing.Point(6, 19);
            this.listsView.MultiSelect = false;
            this.listsView.Name = "listsView";
            this.listsView.ReadOnly = true;
            this.listsView.RowHeadersVisible = false;
            this.listsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listsView.Size = new System.Drawing.Size(607, 139);
            this.listsView.TabIndex = 0;
            this.listsView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.packetView_CellDoubleClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "№";
            this.id.MaxInputLength = 50;
            this.id.MinimumWidth = 50;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 50;
            // 
            // list_type
            // 
            this.list_type.DataPropertyName = "name";
            this.list_type.HeaderText = "Тип пакета";
            this.list_type.Name = "list_type";
            this.list_type.ReadOnly = true;
            this.list_type.Width = 150;
            // 
            // operatorRegColumn
            // 
            this.operatorRegColumn.DataPropertyName = "name_reg";
            this.operatorRegColumn.HeaderText = "Ввёл";
            this.operatorRegColumn.MinimumWidth = 100;
            this.operatorRegColumn.Name = "operatorRegColumn";
            this.operatorRegColumn.ReadOnly = true;
            // 
            // datecreateColumn
            // 
            this.datecreateColumn.DataPropertyName = "reg_date";
            this.datecreateColumn.HeaderText = "Дата создания";
            this.datecreateColumn.MaxInputLength = 50;
            this.datecreateColumn.MinimumWidth = 80;
            this.datecreateColumn.Name = "datecreateColumn";
            this.datecreateColumn.ReadOnly = true;
            // 
            // operatorEditColumn
            // 
            this.operatorEditColumn.DataPropertyName = "name_change";
            this.operatorEditColumn.HeaderText = "Отредактировал";
            this.operatorEditColumn.Name = "operatorEditColumn";
            this.operatorEditColumn.ReadOnly = true;
            // 
            // dateredactColumn
            // 
            this.dateredactColumn.DataPropertyName = "change_date";
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
            this.groupBox3.Controls.Add(this.docCountBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.changeTypedDocButton);
            this.groupBox3.Controls.Add(this.printFormButton);
            this.groupBox3.Controls.Add(this.moveDocButton);
            this.groupBox3.Controls.Add(this.removeDocButton);
            this.groupBox3.Controls.Add(this.editDocButton);
            this.groupBox3.Controls.Add(this.addDocButton);
            this.groupBox3.Controls.Add(this.docView);
            this.groupBox3.Location = new System.Drawing.Point(10, 225);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(914, 294);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Документы СЗВ-1";
            // 
            // docCountBox
            // 
            this.docCountBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.docCountBox.Location = new System.Drawing.Point(786, 262);
            this.docCountBox.Name = "docCountBox";
            this.docCountBox.Size = new System.Drawing.Size(28, 13);
            this.docCountBox.TabIndex = 12;
            this.docCountBox.Text = "0";
            this.docCountBox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(660, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Документов в пакете:";
            // 
            // changeTypedDocButton
            // 
            this.changeTypedDocButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeTypedDocButton.Location = new System.Drawing.Point(6, 265);
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
            this.printFormButton.Location = new System.Drawing.Point(221, 265);
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
            this.moveDocButton.Location = new System.Drawing.Point(820, 146);
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
            this.removeDocButton.Location = new System.Drawing.Point(820, 77);
            this.removeDocButton.Name = "removeDocButton";
            this.removeDocButton.Size = new System.Drawing.Size(88, 23);
            this.removeDocButton.TabIndex = 5;
            this.removeDocButton.Text = "Удалить";
            this.removeDocButton.UseVisualStyleBackColor = true;
            this.removeDocButton.Click += new System.EventHandler(this.removeDocButton_Click);
            // 
            // editDocButton
            // 
            this.editDocButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editDocButton.Location = new System.Drawing.Point(820, 48);
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
            this.addDocButton.Location = new System.Drawing.Point(820, 19);
            this.addDocButton.Name = "addDocButton";
            this.addDocButton.Size = new System.Drawing.Size(88, 23);
            this.addDocButton.TabIndex = 3;
            this.addDocButton.Text = "Добавить";
            this.addDocButton.UseVisualStyleBackColor = true;
            this.addDocButton.Click += new System.EventHandler(this.addDocButton_Click);
            // 
            // docView
            // 
            this.docView.AllowUserToAddRows = false;
            this.docView.AllowUserToDeleteRows = false;
            this.docView.AllowUserToResizeRows = false;
            this.docView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.docView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.docView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.docView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.docView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.docView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.docView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.socNumColumn,
            this.fioColumn,
            this.typeformColumn,
            this.categoryColumn,
            this.operRegColumn,
            this.regDateColumn,
            this.operChangeColumn,
            this.changeDateColumn});
            this.docView.Location = new System.Drawing.Point(6, 19);
            this.docView.Name = "docView";
            this.docView.RowHeadersVisible = false;
            this.docView.RowHeadersWidth = 12;
            this.docView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.docView.Size = new System.Drawing.Size(808, 240);
            this.docView.TabIndex = 0;
            this.docView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.documentView_CellDoubleClick);
            // 
            // checkColumn
            // 
            this.checkColumn.DataPropertyName = "CHECK";
            this.checkColumn.HeaderText = "*";
            this.checkColumn.MinimumWidth = 25;
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.Width = 25;
            // 
            // socNumColumn
            // 
            this.socNumColumn.DataPropertyName = "soc_number";
            this.socNumColumn.HeaderText = "Страховой №";
            this.socNumColumn.MaxInputLength = 100;
            this.socNumColumn.MinimumWidth = 80;
            this.socNumColumn.Name = "socNumColumn";
            this.socNumColumn.ReadOnly = true;
            this.socNumColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.socNumColumn.Width = 91;
            // 
            // fioColumn
            // 
            this.fioColumn.DataPropertyName = "fio";
            this.fioColumn.HeaderText = "Фамилия И.О.";
            this.fioColumn.MaxInputLength = 200;
            this.fioColumn.MinimumWidth = 120;
            this.fioColumn.Name = "fioColumn";
            this.fioColumn.ReadOnly = true;
            this.fioColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fioColumn.Width = 120;
            // 
            // typeformColumn
            // 
            this.typeformColumn.DataPropertyName = "name";
            this.typeformColumn.HeaderText = "Тип формы СЗВ";
            this.typeformColumn.MaxInputLength = 200;
            this.typeformColumn.MinimumWidth = 100;
            this.typeformColumn.Name = "typeformColumn";
            this.typeformColumn.ReadOnly = true;
            this.typeformColumn.Width = 105;
            // 
            // categoryColumn
            // 
            this.categoryColumn.HeaderText = "Категория";
            this.categoryColumn.MaxInputLength = 100;
            this.categoryColumn.MinimumWidth = 70;
            this.categoryColumn.Name = "categoryColumn";
            this.categoryColumn.ReadOnly = true;
            this.categoryColumn.Width = 85;
            // 
            // operRegColumn
            // 
            this.operRegColumn.DataPropertyName = "name_reg";
            this.operRegColumn.HeaderText = "Ввел";
            this.operRegColumn.MaxInputLength = 100;
            this.operRegColumn.MinimumWidth = 100;
            this.operRegColumn.Name = "operRegColumn";
            this.operRegColumn.ReadOnly = true;
            // 
            // regDateColumn
            // 
            this.regDateColumn.DataPropertyName = "reg_date";
            this.regDateColumn.HeaderText = "Дата создания";
            this.regDateColumn.MaxInputLength = 50;
            this.regDateColumn.MinimumWidth = 70;
            this.regDateColumn.Name = "regDateColumn";
            this.regDateColumn.ReadOnly = true;
            // 
            // operChangeColumn
            // 
            this.operChangeColumn.DataPropertyName = "name_change";
            this.operChangeColumn.HeaderText = "Отредактировал";
            this.operChangeColumn.Name = "operChangeColumn";
            this.operChangeColumn.ReadOnly = true;
            this.operChangeColumn.Width = 116;
            // 
            // changeDateColumn
            // 
            this.changeDateColumn.DataPropertyName = "change_date";
            this.changeDateColumn.HeaderText = "Дата редактирования";
            this.changeDateColumn.MaxInputLength = 50;
            this.changeDateColumn.MinimumWidth = 80;
            this.changeDateColumn.Name = "changeDateColumn";
            this.changeDateColumn.ReadOnly = true;
            this.changeDateColumn.Width = 132;
            // 
            // StajDohodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(933, 529);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(800, 567);
            this.Name = "StajDohodForm";
            this.Text = "Стаж и доход";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StajDohodForm_FormClosing);
            this.Load += new System.EventHandler(this.StajDohodForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listsView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown yearBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView listsView;
        private System.Windows.Forms.DataGridView docView;
        private System.Windows.Forms.Button movePacketYearButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Button reestrButton;
        private System.Windows.Forms.Button movePacketOrgButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button moveDocButton;
        private System.Windows.Forms.Button removeDocButton;
        private System.Windows.Forms.Button editDocButton;
        private System.Windows.Forms.Button addDocButton;
        private System.Windows.Forms.Label packetcountBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button changeTypedDocButton;
        private System.Windows.Forms.Button printFormButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn list_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatorRegColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datecreateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatorEditColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateredactColumn;
        private System.Windows.Forms.Label docCountBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn socNumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fioColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeformColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operRegColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operChangeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn changeDateColumn;
    }
}