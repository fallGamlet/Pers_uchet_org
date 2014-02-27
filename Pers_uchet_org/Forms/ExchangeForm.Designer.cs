namespace Pers_uchet_org
{
    partial class ExchangeForm
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
            this.ExchangeTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPageDB = new System.Windows.Forms.TabPage();
            this.chekedDocCountBox = new System.Windows.Forms.TextBox();
            this.checkedPacketCountBox = new System.Windows.Forms.TextBox();
            this.szvGroupBox = new System.Windows.Forms.GroupBox();
            this.docCountBox = new System.Windows.Forms.TextBox();
            this.packetCountBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.packetsView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.packetNumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageXML = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.xmlPathButton = new System.Windows.Forms.Button();
            this.xmlPathTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.keyfileTextBox = new System.Windows.Forms.TextBox();
            this.keyfileButton = new System.Windows.Forms.Button();
            this.keyfileRButton = new System.Windows.Forms.RadioButton();
            this.cdRButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.driveBox = new System.Windows.Forms.ComboBox();
            this.viewdataButton = new System.Windows.Forms.Button();
            this.flashRButton = new System.Windows.Forms.RadioButton();
            this.yearGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.yearBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flashBox = new System.Windows.Forms.ComboBox();
            this.internetRButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.progresLabel = new System.Windows.Forms.Label();
            this.keyDateLabel = new System.Windows.Forms.Label();
            this.createDataFileButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.senddataButton = new System.Windows.Forms.Button();
            this.ExchangeTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPageDB.SuspendLayout();
            this.szvGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetsView)).BeginInit();
            this.tabPageXML.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.yearGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExchangeTabControl
            // 
            this.ExchangeTabControl.Controls.Add(this.tabPage1);
            this.ExchangeTabControl.Controls.Add(this.tabPageDB);
            this.ExchangeTabControl.Controls.Add(this.tabPageXML);
            this.ExchangeTabControl.Location = new System.Drawing.Point(12, 70);
            this.ExchangeTabControl.Multiline = true;
            this.ExchangeTabControl.Name = "ExchangeTabControl";
            this.ExchangeTabControl.SelectedIndex = 0;
            this.ExchangeTabControl.Size = new System.Drawing.Size(306, 424);
            this.ExchangeTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(298, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Регистрационные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(286, 60);
            this.label8.TabIndex = 3;
            this.label8.Text = "КОМПОНЕНТ НАХОДИТСЯ В РАЗРАБОТКЕ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageDB
            // 
            this.tabPageDB.Controls.Add(this.chekedDocCountBox);
            this.tabPageDB.Controls.Add(this.checkedPacketCountBox);
            this.tabPageDB.Controls.Add(this.szvGroupBox);
            this.tabPageDB.Controls.Add(this.label5);
            this.tabPageDB.Controls.Add(this.label4);
            this.tabPageDB.Controls.Add(this.packetsView);
            this.tabPageDB.Controls.Add(this.label3);
            this.tabPageDB.Location = new System.Drawing.Point(4, 22);
            this.tabPageDB.Name = "tabPageDB";
            this.tabPageDB.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDB.Size = new System.Drawing.Size(298, 398);
            this.tabPageDB.TabIndex = 1;
            this.tabPageDB.Text = "Стаж и доход";
            this.tabPageDB.UseVisualStyleBackColor = true;
            // 
            // chekedDocCountBox
            // 
            this.chekedDocCountBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chekedDocCountBox.Location = new System.Drawing.Point(238, 316);
            this.chekedDocCountBox.Name = "chekedDocCountBox";
            this.chekedDocCountBox.ReadOnly = true;
            this.chekedDocCountBox.Size = new System.Drawing.Size(54, 20);
            this.chekedDocCountBox.TabIndex = 10;
            this.chekedDocCountBox.Text = "0";
            this.chekedDocCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkedPacketCountBox
            // 
            this.checkedPacketCountBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkedPacketCountBox.Location = new System.Drawing.Point(105, 316);
            this.checkedPacketCountBox.Name = "checkedPacketCountBox";
            this.checkedPacketCountBox.ReadOnly = true;
            this.checkedPacketCountBox.Size = new System.Drawing.Size(45, 20);
            this.checkedPacketCountBox.TabIndex = 9;
            this.checkedPacketCountBox.Text = "0";
            this.checkedPacketCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // szvGroupBox
            // 
            this.szvGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.szvGroupBox.Controls.Add(this.docCountBox);
            this.szvGroupBox.Controls.Add(this.packetCountBox);
            this.szvGroupBox.Controls.Add(this.label7);
            this.szvGroupBox.Controls.Add(this.label6);
            this.szvGroupBox.Location = new System.Drawing.Point(6, 342);
            this.szvGroupBox.Name = "szvGroupBox";
            this.szvGroupBox.Size = new System.Drawing.Size(289, 50);
            this.szvGroupBox.TabIndex = 8;
            this.szvGroupBox.TabStop = false;
            this.szvGroupBox.Text = "Сводная ведомость (СЗВ-3)";
            // 
            // docCountBox
            // 
            this.docCountBox.Location = new System.Drawing.Point(232, 19);
            this.docCountBox.Name = "docCountBox";
            this.docCountBox.ReadOnly = true;
            this.docCountBox.Size = new System.Drawing.Size(54, 20);
            this.docCountBox.TabIndex = 8;
            this.docCountBox.Text = "0";
            this.docCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // packetCountBox
            // 
            this.packetCountBox.Location = new System.Drawing.Point(99, 19);
            this.packetCountBox.Name = "packetCountBox";
            this.packetCountBox.ReadOnly = true;
            this.packetCountBox.Size = new System.Drawing.Size(45, 20);
            this.packetCountBox.TabIndex = 7;
            this.packetCountBox.Text = "0";
            this.packetCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(150, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = ", документов";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Пакетов";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 319);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = ",  документов";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Выбрано пакетов";
            // 
            // packetsView
            // 
            this.packetsView.AllowUserToAddRows = false;
            this.packetsView.AllowUserToDeleteRows = false;
            this.packetsView.AllowUserToResizeRows = false;
            this.packetsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.packetsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.packetsView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.packetsView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.packetsView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.packetsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.packetNumColumn,
            this.docCountColumn});
            this.packetsView.Location = new System.Drawing.Point(6, 61);
            this.packetsView.Name = "packetsView";
            this.packetsView.RowHeadersVisible = false;
            this.packetsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.packetsView.Size = new System.Drawing.Size(286, 249);
            this.packetsView.TabIndex = 2;
            this.packetsView.Sorted += new System.EventHandler(this.packetsView_Sorted);
            this.packetsView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.packetsView_CellClick);
            this.packetsView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.packetsView_KeyDown);
            this.packetsView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.packetsView_CellContentClick);
            // 
            // checkColumn
            // 
            this.checkColumn.FillWeight = 30F;
            this.checkColumn.HeaderText = "*";
            this.checkColumn.MinimumWidth = 20;
            this.checkColumn.Name = "checkColumn";
            // 
            // packetNumColumn
            // 
            this.packetNumColumn.FillWeight = 127F;
            this.packetNumColumn.HeaderText = "№ пакета";
            this.packetNumColumn.MaxInputLength = 50;
            this.packetNumColumn.MinimumWidth = 30;
            this.packetNumColumn.Name = "packetNumColumn";
            this.packetNumColumn.ReadOnly = true;
            // 
            // docCountColumn
            // 
            this.docCountColumn.FillWeight = 127F;
            this.docCountColumn.HeaderText = "Количество документов";
            this.docCountColumn.MaxInputLength = 50;
            this.docCountColumn.MinimumWidth = 100;
            this.docCountColumn.Name = "docCountColumn";
            this.docCountColumn.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(286, 55);
            this.label3.TabIndex = 0;
            this.label3.Text = "Выберите пакеты документов СЗВ-1, \r\nкоторые Вы собираетесь предоставить в отделен" +
                "ие Единого фонда социального страхования ПМР";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageXML
            // 
            this.tabPageXML.Controls.Add(this.label11);
            this.tabPageXML.Controls.Add(this.label10);
            this.tabPageXML.Controls.Add(this.xmlPathButton);
            this.tabPageXML.Controls.Add(this.xmlPathTextBox);
            this.tabPageXML.Controls.Add(this.label9);
            this.tabPageXML.Location = new System.Drawing.Point(4, 22);
            this.tabPageXML.Name = "tabPageXML";
            this.tabPageXML.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageXML.Size = new System.Drawing.Size(298, 398);
            this.tabPageXML.TabIndex = 2;
            this.tabPageXML.Text = "XML-файлы";
            this.tabPageXML.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(6, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(286, 60);
            this.label11.TabIndex = 13;
            this.label11.Text = "С описанием структуры и месторасположения XML-файлов можно ознакомиться в справоч" +
                "ной системе программы.";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(6, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(286, 91);
            this.label10.TabIndex = 12;
            this.label10.Text = "Модуль упаковки XML-файлов в электронный контейнер осуществляет предварительную п" +
                "роверку заранее подготовленных XML-файлов, шифрование, упаковку файлов в электро" +
                "нный контейнер и электронную подпись.";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xmlPathButton
            // 
            this.xmlPathButton.Location = new System.Drawing.Point(249, 17);
            this.xmlPathButton.Name = "xmlPathButton";
            this.xmlPathButton.Size = new System.Drawing.Size(43, 23);
            this.xmlPathButton.TabIndex = 11;
            this.xmlPathButton.Text = "...";
            this.xmlPathButton.UseVisualStyleBackColor = true;
            this.xmlPathButton.Click += new System.EventHandler(this.xmlPathButton_Click);
            // 
            // xmlPathTextBox
            // 
            this.xmlPathTextBox.Location = new System.Drawing.Point(6, 19);
            this.xmlPathTextBox.Name = "xmlPathTextBox";
            this.xmlPathTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.xmlPathTextBox.Size = new System.Drawing.Size(237, 20);
            this.xmlPathTextBox.TabIndex = 10;
            this.xmlPathTextBox.Text = "C:\\XML-ROOT\\";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Место размещения XML-файлов";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.keyfileTextBox);
            this.groupBox1.Controls.Add(this.keyfileButton);
            this.groupBox1.Controls.Add(this.keyfileRButton);
            this.groupBox1.Controls.Add(this.cdRButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.driveBox);
            this.groupBox1.Location = new System.Drawing.Point(324, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 204);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1    Укажите источник ключевой информации";
            // 
            // keyfileTextBox
            // 
            this.keyfileTextBox.Location = new System.Drawing.Point(72, 165);
            this.keyfileTextBox.Name = "keyfileTextBox";
            this.keyfileTextBox.ReadOnly = true;
            this.keyfileTextBox.Size = new System.Drawing.Size(261, 20);
            this.keyfileTextBox.TabIndex = 4;
            // 
            // keyfileButton
            // 
            this.keyfileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.keyfileButton.Location = new System.Drawing.Point(339, 163);
            this.keyfileButton.Name = "keyfileButton";
            this.keyfileButton.Size = new System.Drawing.Size(64, 23);
            this.keyfileButton.TabIndex = 5;
            this.keyfileButton.Text = "Обзор...";
            this.keyfileButton.UseVisualStyleBackColor = true;
            this.keyfileButton.Click += new System.EventHandler(this.keyfileButton_Click);
            // 
            // keyfileRButton
            // 
            this.keyfileRButton.AutoSize = true;
            this.keyfileRButton.Location = new System.Drawing.Point(12, 166);
            this.keyfileRButton.Name = "keyfileRButton";
            this.keyfileRButton.Size = new System.Drawing.Size(54, 17);
            this.keyfileRButton.TabIndex = 2;
            this.keyfileRButton.Text = "Файл";
            this.keyfileRButton.UseVisualStyleBackColor = true;
            this.keyfileRButton.CheckedChanged += new System.EventHandler(this.cdOrKeyfileRButton_CheckedChanged);
            // 
            // cdRButton
            // 
            this.cdRButton.AutoSize = true;
            this.cdRButton.Location = new System.Drawing.Point(12, 22);
            this.cdRButton.Name = "cdRButton";
            this.cdRButton.Size = new System.Drawing.Size(124, 17);
            this.cdRButton.TabIndex = 1;
            this.cdRButton.Text = "Оптический привод";
            this.cdRButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(29, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(374, 59);
            this.label1.TabIndex = 2;
            this.label1.Text = "Вставьте ключевой диск вашей организации (полученный в Управлении персонифицирова" +
                "нного учета Единого фонда социального страхования ПМР), и укажите букву диска";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // driveBox
            // 
            this.driveBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.driveBox.FormattingEnabled = true;
            this.driveBox.Location = new System.Drawing.Point(178, 113);
            this.driveBox.Name = "driveBox";
            this.driveBox.Size = new System.Drawing.Size(225, 21);
            this.driveBox.TabIndex = 3;
            this.driveBox.Click += new System.EventHandler(this.driveBox_Click);
            // 
            // viewdataButton
            // 
            this.viewdataButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.viewdataButton.Location = new System.Drawing.Point(6, 19);
            this.viewdataButton.Name = "viewdataButton";
            this.viewdataButton.Size = new System.Drawing.Size(397, 23);
            this.viewdataButton.TabIndex = 3;
            this.viewdataButton.Text = "Просмотреть файл электронных данных";
            this.viewdataButton.UseVisualStyleBackColor = true;
            this.viewdataButton.Click += new System.EventHandler(this.viewDataButton_Click);
            // 
            // flashRButton
            // 
            this.flashRButton.AutoSize = true;
            this.flashRButton.Checked = true;
            this.flashRButton.Location = new System.Drawing.Point(12, 28);
            this.flashRButton.Name = "flashRButton";
            this.flashRButton.Size = new System.Drawing.Size(118, 17);
            this.flashRButton.TabIndex = 5;
            this.flashRButton.TabStop = true;
            this.flashRButton.Text = "Флеш накопитель";
            this.flashRButton.UseVisualStyleBackColor = true;
            this.flashRButton.CheckedChanged += new System.EventHandler(this.flashRButton_CheckedChanged);
            // 
            // yearGroupBox
            // 
            this.yearGroupBox.Controls.Add(this.label2);
            this.yearGroupBox.Controls.Add(this.yearBox);
            this.yearGroupBox.Location = new System.Drawing.Point(68, 12);
            this.yearGroupBox.Name = "yearGroupBox";
            this.yearGroupBox.Size = new System.Drawing.Size(181, 52);
            this.yearGroupBox.TabIndex = 7;
            this.yearGroupBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Отчетный год";
            // 
            // yearBox
            // 
            this.yearBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yearBox.Location = new System.Drawing.Point(90, 19);
            this.yearBox.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.yearBox.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(85, 20);
            this.yearBox.TabIndex = 0;
            this.yearBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.yearBox.Value = new decimal(new int[] {
            2013,
            0,
            0,
            0});
            this.yearBox.ValueChanged += new System.EventHandler(this.yearBox_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flashBox);
            this.groupBox2.Controls.Add(this.internetRButton);
            this.groupBox2.Controls.Add(this.flashRButton);
            this.groupBox2.Location = new System.Drawing.Point(324, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 76);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2    Укажите способ передачи";
            // 
            // flashBox
            // 
            this.flashBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flashBox.FormattingEnabled = true;
            this.flashBox.Location = new System.Drawing.Point(178, 27);
            this.flashBox.Name = "flashBox";
            this.flashBox.Size = new System.Drawing.Size(225, 21);
            this.flashBox.TabIndex = 7;
            this.flashBox.Click += new System.EventHandler(this.flashBox_Click);
            // 
            // internetRButton
            // 
            this.internetRButton.AutoSize = true;
            this.internetRButton.Location = new System.Drawing.Point(12, 51);
            this.internetRButton.Name = "internetRButton";
            this.internetRButton.Size = new System.Drawing.Size(73, 17);
            this.internetRButton.TabIndex = 6;
            this.internetRButton.Text = "Интернет";
            this.internetRButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.progresLabel);
            this.groupBox3.Controls.Add(this.keyDateLabel);
            this.groupBox3.Controls.Add(this.createDataFileButton);
            this.groupBox3.Location = new System.Drawing.Point(324, 304);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(409, 67);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3";
            // 
            // progresLabel
            // 
            this.progresLabel.AutoSize = true;
            this.progresLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.progresLabel.Location = new System.Drawing.Point(302, 45);
            this.progresLabel.Name = "progresLabel";
            this.progresLabel.Size = new System.Drawing.Size(95, 16);
            this.progresLabel.TabIndex = 6;
            this.progresLabel.Text = "Прогресс: 0%";
            // 
            // keyDateLabel
            // 
            this.keyDateLabel.AutoSize = true;
            this.keyDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.keyDateLabel.Location = new System.Drawing.Point(55, 45);
            this.keyDateLabel.Name = "keyDateLabel";
            this.keyDateLabel.Size = new System.Drawing.Size(207, 16);
            this.keyDateLabel.TabIndex = 5;
            this.keyDateLabel.Tag = "Ключ действителен с {0} до {1}";
            this.keyDateLabel.Text = "Ключ действителен с {0} до {1}";
            // 
            // createDataFileButton
            // 
            this.createDataFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.createDataFileButton.Location = new System.Drawing.Point(6, 19);
            this.createDataFileButton.Name = "createDataFileButton";
            this.createDataFileButton.Size = new System.Drawing.Size(397, 23);
            this.createDataFileButton.TabIndex = 4;
            this.createDataFileButton.Text = "Сформировать файл электронных данных";
            this.createDataFileButton.UseVisualStyleBackColor = true;
            this.createDataFileButton.Click += new System.EventHandler(this.createDataFileButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.viewdataButton);
            this.groupBox4.Location = new System.Drawing.Point(324, 377);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(409, 56);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "4";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.senddataButton);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(324, 439);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(409, 55);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "5";
            // 
            // senddataButton
            // 
            this.senddataButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.senddataButton.Location = new System.Drawing.Point(6, 19);
            this.senddataButton.Name = "senddataButton";
            this.senddataButton.Size = new System.Drawing.Size(397, 23);
            this.senddataButton.TabIndex = 4;
            this.senddataButton.Text = "Отправить данные в ЕГФСС через Интернет";
            this.senddataButton.UseVisualStyleBackColor = true;
            this.senddataButton.Click += new System.EventHandler(this.sendDataButton_Click);
            // 
            // ExchangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 500);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.yearGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ExchangeTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ExchangeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Электронный обмен с фондом";
            this.Load += new System.EventHandler(this.ExchangeForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExchangeForm_FormClosing);
            this.ExchangeTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPageDB.ResumeLayout(false);
            this.tabPageDB.PerformLayout();
            this.szvGroupBox.ResumeLayout(false);
            this.szvGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetsView)).EndInit();
            this.tabPageXML.ResumeLayout(false);
            this.tabPageXML.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.yearGroupBox.ResumeLayout(false);
            this.yearGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ExchangeTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPageDB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPageXML;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button viewdataButton;
        private System.Windows.Forms.RadioButton flashRButton;
        private System.Windows.Forms.ComboBox driveBox;
        private System.Windows.Forms.GroupBox yearGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown yearBox;
        private System.Windows.Forms.DataGridView packetsView;
        private System.Windows.Forms.GroupBox szvGroupBox;
        private System.Windows.Forms.TextBox docCountBox;
        private System.Windows.Forms.TextBox packetCountBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox chekedDocCountBox;
        private System.Windows.Forms.TextBox checkedPacketCountBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button createDataFileButton;
        private System.Windows.Forms.Button senddataButton;
        private System.Windows.Forms.RadioButton internetRButton;
        private System.Windows.Forms.ComboBox flashBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button xmlPathButton;
        private System.Windows.Forms.TextBox xmlPathTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label keyDateLabel;
        private System.Windows.Forms.Label progresLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetNumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn docCountColumn;
        private System.Windows.Forms.Label szv3WarningLabel;
        private System.Windows.Forms.RadioButton keyfileRButton;
        private System.Windows.Forms.RadioButton cdRButton;
        private System.Windows.Forms.Button keyfileButton;
        private System.Windows.Forms.TextBox keyfileTextBox;
    }
}