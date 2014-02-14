namespace Pers_uchet_org
{
    partial class AddEditSpecialPeriodForm
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
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.monthsNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.daysNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.minutesNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.hoursNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.professionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.servYearBaseComboBox = new System.Windows.Forms.ComboBox();
            this.stajBaseComboBox = new System.Windows.Forms.ComboBox();
            this.partConditionComboBox = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.beginDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthsNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.daysNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(187, 283);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 13;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(268, 283);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.monthsNumUpDown);
            this.groupBox1.Controls.Add(this.daysNumUpDown);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.minutesNumUpDown);
            this.groupBox1.Controls.Add(this.hoursNumUpDown);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Location = new System.Drawing.Point(12, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 68);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дополнительные сведения (количество)";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 21);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(48, 13);
            this.label27.TabIndex = 0;
            this.label27.Text = "Месяцы";
            // 
            // monthsNumUpDown
            // 
            this.monthsNumUpDown.Location = new System.Drawing.Point(9, 37);
            this.monthsNumUpDown.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.monthsNumUpDown.Name = "monthsNumUpDown";
            this.monthsNumUpDown.Size = new System.Drawing.Size(74, 20);
            this.monthsNumUpDown.TabIndex = 4;
            this.monthsNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthsNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDownFirst_ValueChanged);
            // 
            // daysNumUpDown
            // 
            this.daysNumUpDown.Location = new System.Drawing.Point(89, 37);
            this.daysNumUpDown.Maximum = new decimal(new int[] {
            366,
            0,
            0,
            0});
            this.daysNumUpDown.Name = "daysNumUpDown";
            this.daysNumUpDown.Size = new System.Drawing.Size(74, 20);
            this.daysNumUpDown.TabIndex = 5;
            this.daysNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.daysNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDownFirst_ValueChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(246, 21);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(46, 13);
            this.label30.TabIndex = 3;
            this.label30.Text = "Минуты";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(86, 21);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(28, 13);
            this.label28.TabIndex = 1;
            this.label28.Text = "Дни";
            // 
            // minutesNumUpDown
            // 
            this.minutesNumUpDown.Location = new System.Drawing.Point(249, 37);
            this.minutesNumUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minutesNumUpDown.Name = "minutesNumUpDown";
            this.minutesNumUpDown.Size = new System.Drawing.Size(74, 20);
            this.minutesNumUpDown.TabIndex = 7;
            this.minutesNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.minutesNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDownSecond_ValueChanged);
            // 
            // hoursNumUpDown
            // 
            this.hoursNumUpDown.Location = new System.Drawing.Point(169, 37);
            this.hoursNumUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.hoursNumUpDown.Name = "hoursNumUpDown";
            this.hoursNumUpDown.Size = new System.Drawing.Size(74, 20);
            this.hoursNumUpDown.TabIndex = 6;
            this.hoursNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hoursNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDownSecond_ValueChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(166, 21);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(35, 13);
            this.label29.TabIndex = 2;
            this.label29.Text = "Часы";
            // 
            // professionRichTextBox
            // 
            this.professionRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.professionRichTextBox.Location = new System.Drawing.Point(15, 226);
            this.professionRichTextBox.Name = "professionRichTextBox";
            this.professionRichTextBox.Size = new System.Drawing.Size(328, 51);
            this.professionRichTextBox.TabIndex = 12;
            this.professionRichTextBox.Text = "";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(12, 210);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(130, 13);
            this.label31.TabIndex = 11;
            this.label31.Text = "Должность (профессия)";
            // 
            // servYearBaseComboBox
            // 
            this.servYearBaseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.servYearBaseComboBox.FormattingEnabled = true;
            this.servYearBaseComboBox.Location = new System.Drawing.Point(187, 112);
            this.servYearBaseComboBox.Name = "servYearBaseComboBox";
            this.servYearBaseComboBox.Size = new System.Drawing.Size(156, 21);
            this.servYearBaseComboBox.TabIndex = 9;
            // 
            // stajBaseComboBox
            // 
            this.stajBaseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stajBaseComboBox.FormattingEnabled = true;
            this.stajBaseComboBox.Location = new System.Drawing.Point(187, 85);
            this.stajBaseComboBox.Name = "stajBaseComboBox";
            this.stajBaseComboBox.Size = new System.Drawing.Size(156, 21);
            this.stajBaseComboBox.TabIndex = 8;
            // 
            // partConditionComboBox
            // 
            this.partConditionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partConditionComboBox.FormattingEnabled = true;
            this.partConditionComboBox.Location = new System.Drawing.Point(187, 58);
            this.partConditionComboBox.Name = "partConditionComboBox";
            this.partConditionComboBox.Size = new System.Drawing.Size(156, 21);
            this.partConditionComboBox.TabIndex = 7;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 35);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(83, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Конец периода";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 9);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Начало периода";
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDateTimePicker.Location = new System.Drawing.Point(187, 32);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(156, 20);
            this.endDateTimePicker.TabIndex = 6;
            this.endDateTimePicker.ValueChanged += new System.EventHandler(this.TimePicker_ValueChanged);
            // 
            // beginDateTimePicker
            // 
            this.beginDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.beginDateTimePicker.Location = new System.Drawing.Point(187, 6);
            this.beginDateTimePicker.Name = "beginDateTimePicker";
            this.beginDateTimePicker.Size = new System.Drawing.Size(156, 20);
            this.beginDateTimePicker.TabIndex = 5;
            this.beginDateTimePicker.ValueChanged += new System.EventHandler(this.TimePicker_ValueChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(15, 59);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(167, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Text = "Особые условия труда (код)";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(15, 86);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(164, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Трудовой стаж (основание)";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(15, 113);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(151, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.Text = "Выслуга лет (основание)";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // AddEditSpecialPeriodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(355, 318);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.professionRichTextBox);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.servYearBaseComboBox);
            this.Controls.Add(this.stajBaseComboBox);
            this.Controls.Add(this.partConditionComboBox);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.endDateTimePicker);
            this.Controls.Add(this.beginDateTimePicker);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditSpecialPeriodForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление периода специального стажа";
            this.Load += new System.EventHandler(this.AddEditSpecialPeriodForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthsNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.daysNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursNumUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown monthsNumUpDown;
        private System.Windows.Forms.NumericUpDown daysNumUpDown;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.NumericUpDown minutesNumUpDown;
        private System.Windows.Forms.NumericUpDown hoursNumUpDown;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.RichTextBox professionRichTextBox;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox servYearBaseComboBox;
        private System.Windows.Forms.ComboBox stajBaseComboBox;
        private System.Windows.Forms.ComboBox partConditionComboBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.DateTimePicker beginDateTimePicker;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}