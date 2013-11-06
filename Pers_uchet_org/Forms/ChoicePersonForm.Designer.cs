namespace Pers_uchet_org
{
    partial class ChoicePersonForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.countBox = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rawAnketsButton = new System.Windows.Forms.Button();
            this.allAnketsButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.personView = new System.Windows.Forms.DataGridView();
            this.numColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.searchFioTextBox = new System.Windows.Forms.TextBox();
            this.searchNumTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.choiceButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personView)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // countBox
            // 
            this.countBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countBox.Location = new System.Drawing.Point(53, 161);
            this.countBox.Name = "countBox";
            this.countBox.Size = new System.Drawing.Size(41, 16);
            this.countBox.TabIndex = 10;
            this.countBox.Text = "0";
            this.countBox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Всего: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rawAnketsButton);
            this.groupBox2.Controls.Add(this.allAnketsButton);
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox2.Size = new System.Drawing.Size(344, 51);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // rawAnketsButton
            // 
            this.rawAnketsButton.Location = new System.Drawing.Point(160, 17);
            this.rawAnketsButton.Name = "rawAnketsButton";
            this.rawAnketsButton.Size = new System.Drawing.Size(180, 23);
            this.rawAnketsButton.TabIndex = 9;
            this.rawAnketsButton.Text = "Необработанные анкеты";
            this.rawAnketsButton.UseVisualStyleBackColor = true;
            this.rawAnketsButton.Click += new System.EventHandler(this.rawAnketsButton_Click);
            // 
            // allAnketsButton
            // 
            this.allAnketsButton.Location = new System.Drawing.Point(4, 17);
            this.allAnketsButton.Name = "allAnketsButton";
            this.allAnketsButton.Size = new System.Drawing.Size(150, 23);
            this.allAnketsButton.TabIndex = 8;
            this.allAnketsButton.Text = "Все анкеты";
            this.allAnketsButton.UseVisualStyleBackColor = true;
            this.allAnketsButton.Click += new System.EventHandler(this.allAnketsButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.countBox);
            this.groupBox3.Controls.Add(this.personView);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.searchFioTextBox);
            this.groupBox3.Controls.Add(this.searchNumTextBox);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(10, 63);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox3.Size = new System.Drawing.Size(344, 179);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // personView
            // 
            this.personView.AllowUserToAddRows = false;
            this.personView.AllowUserToDeleteRows = false;
            this.personView.AllowUserToResizeRows = false;
            this.personView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.personView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.personView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.personView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.personView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numColumn,
            this.fioColumn});
            this.personView.Location = new System.Drawing.Point(7, 56);
            this.personView.MultiSelect = false;
            this.personView.Name = "personView";
            this.personView.ReadOnly = true;
            this.personView.RowHeadersVisible = false;
            this.personView.RowHeadersWidth = 15;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.personView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.personView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.personView.Size = new System.Drawing.Size(333, 102);
            this.personView.TabIndex = 4;
            this.personView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.personView_CellDoubleClick);
            this.personView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.personView_KeyDown);
            // 
            // numColumn
            // 
            this.numColumn.DataPropertyName = "soc_number";
            this.numColumn.HeaderText = "Страховой №";
            this.numColumn.MaxInputLength = 100;
            this.numColumn.MinimumWidth = 90;
            this.numColumn.Name = "numColumn";
            this.numColumn.ReadOnly = true;
            this.numColumn.Width = 90;
            // 
            // fioColumn
            // 
            this.fioColumn.DataPropertyName = "fio";
            this.fioColumn.HeaderText = "Фамилия И.О.";
            this.fioColumn.MaxInputLength = 150;
            this.fioColumn.MinimumWidth = 200;
            this.fioColumn.Name = "fioColumn";
            this.fioColumn.ReadOnly = true;
            this.fioColumn.Width = 200;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Поиск по фамилии";
            // 
            // searchFioTextBox
            // 
            this.searchFioTextBox.Location = new System.Drawing.Point(121, 30);
            this.searchFioTextBox.MaxLength = 200;
            this.searchFioTextBox.Name = "searchFioTextBox";
            this.searchFioTextBox.Size = new System.Drawing.Size(219, 20);
            this.searchFioTextBox.TabIndex = 3;
            this.searchFioTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchFioTextBox_KeyPress);
            // 
            // searchNumTextBox
            // 
            this.searchNumTextBox.Location = new System.Drawing.Point(7, 30);
            this.searchNumTextBox.MaxLength = 200;
            this.searchNumTextBox.Name = "searchNumTextBox";
            this.searchNumTextBox.Size = new System.Drawing.Size(108, 20);
            this.searchNumTextBox.TabIndex = 2;
            this.searchNumTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchNumTextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Поиск по страх. №";
            // 
            // choiceButton
            // 
            this.choiceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.choiceButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.choiceButton.Location = new System.Drawing.Point(148, 327);
            this.choiceButton.Name = "choiceButton";
            this.choiceButton.Size = new System.Drawing.Size(125, 23);
            this.choiceButton.TabIndex = 8;
            this.choiceButton.Text = "Выбрать";
            this.choiceButton.UseVisualStyleBackColor = true;
            this.choiceButton.Click += new System.EventHandler(this.choiceButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(279, 327);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.radioButton4);
            this.groupBox4.Controls.Add(this.radioButton3);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Location = new System.Drawing.Point(10, 246);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(344, 73);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Enabled = false;
            this.radioButton4.Location = new System.Drawing.Point(160, 42);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(125, 17);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.Text = "Назначение пенсии";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.DocRadioButton_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(160, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(130, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Отменяющая форма";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.DocRadioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(150, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Корректирующая форма";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.DocRadioButton_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(111, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Исходная форма";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.DocRadioButton_CheckedChanged);
            // 
            // ChoicePersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(366, 362);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.choiceButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(382, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(382, 100);
            this.Name = "ChoicePersonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор застрахованного лица";
            this.Load += new System.EventHandler(this.ChoicePersonForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personView)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label countBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView personView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox searchFioTextBox;
        private System.Windows.Forms.TextBox searchNumTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button rawAnketsButton;
        private System.Windows.Forms.Button allAnketsButton;
        private System.Windows.Forms.Button choiceButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn numColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fioColumn;
    }
}