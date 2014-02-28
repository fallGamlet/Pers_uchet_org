namespace Pers_uchet_org.Forms
{
    partial class InvokerForm
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
            this.regNumLabel = new System.Windows.Forms.Label();
            this.repYearLabel = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.reportTypeLabel = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.lanSettingsButton = new System.Windows.Forms.Button();
            this.yearBox = new System.Windows.Forms.NumericUpDown();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.reportTypeComboBox = new System.Windows.Forms.ComboBox();
            this.edataBrowseButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.regNumOrgBox = new System.Windows.Forms.MaskedTextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.showPassButton = new System.Windows.Forms.Button();
            this.logLabel = new System.Windows.Forms.Label();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).BeginInit();
            this.SuspendLayout();
            // 
            // regNumLabel
            // 
            this.regNumLabel.AutoSize = true;
            this.regNumLabel.Location = new System.Drawing.Point(12, 9);
            this.regNumLabel.Name = "regNumLabel";
            this.regNumLabel.Size = new System.Drawing.Size(133, 13);
            this.regNumLabel.TabIndex = 0;
            this.regNumLabel.Text = "Регистрационный номер";
            // 
            // repYearLabel
            // 
            this.repYearLabel.AutoSize = true;
            this.repYearLabel.Location = new System.Drawing.Point(12, 34);
            this.repYearLabel.Name = "repYearLabel";
            this.repYearLabel.Size = new System.Drawing.Size(76, 13);
            this.repYearLabel.TabIndex = 1;
            this.repYearLabel.Text = "Отчетный год";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(12, 61);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(109, 13);
            this.pathLabel.TabIndex = 2;
            this.pathLabel.Text = "Путь к папке с ЭДО";
            // 
            // reportTypeLabel
            // 
            this.reportTypeLabel.AutoSize = true;
            this.reportTypeLabel.Location = new System.Drawing.Point(12, 87);
            this.reportTypeLabel.Name = "reportTypeLabel";
            this.reportTypeLabel.Size = new System.Drawing.Size(119, 13);
            this.reportTypeLabel.TabIndex = 3;
            this.reportTypeLabel.Text = "Тип документа отчета";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(151, 137);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(132, 23);
            this.sendButton.TabIndex = 14;
            this.sendButton.Text = "Отправить";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // lanSettingsButton
            // 
            this.lanSettingsButton.Location = new System.Drawing.Point(77, 137);
            this.lanSettingsButton.Name = "lanSettingsButton";
            this.lanSettingsButton.Size = new System.Drawing.Size(65, 23);
            this.lanSettingsButton.TabIndex = 13;
            this.lanSettingsButton.Text = "Сеть";
            this.lanSettingsButton.UseVisualStyleBackColor = true;
            this.lanSettingsButton.Click += new System.EventHandler(this.lanSettingsButton_Click);
            // 
            // yearBox
            // 
            this.yearBox.Location = new System.Drawing.Point(151, 32);
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
            this.yearBox.Size = new System.Drawing.Size(90, 20);
            this.yearBox.TabIndex = 6;
            this.yearBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.yearBox.Value = new decimal(new int[] {
            2013,
            0,
            0,
            0});
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(151, 58);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(193, 20);
            this.pathTextBox.TabIndex = 7;
            // 
            // reportTypeComboBox
            // 
            this.reportTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reportTypeComboBox.FormattingEnabled = true;
            this.reportTypeComboBox.Items.AddRange(new object[] {
            "Документ WEB (*.html) (рекомендуется)",
            "Документ Adobe Acrobat (*.pdf)",
            "Документ WordPad (*.rtf)"});
            this.reportTypeComboBox.Location = new System.Drawing.Point(151, 84);
            this.reportTypeComboBox.Name = "reportTypeComboBox";
            this.reportTypeComboBox.Size = new System.Drawing.Size(224, 21);
            this.reportTypeComboBox.TabIndex = 9;
            // 
            // edataBrowseButton
            // 
            this.edataBrowseButton.Location = new System.Drawing.Point(350, 56);
            this.edataBrowseButton.Name = "edataBrowseButton";
            this.edataBrowseButton.Size = new System.Drawing.Size(25, 23);
            this.edataBrowseButton.TabIndex = 8;
            this.edataBrowseButton.Text = "...";
            this.edataBrowseButton.UseVisualStyleBackColor = true;
            this.edataBrowseButton.Click += new System.EventHandler(this.edataBrowseButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Справка";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // regNumOrgBox
            // 
            this.regNumOrgBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.regNumOrgBox.Location = new System.Drawing.Point(151, 6);
            this.regNumOrgBox.Mask = ">L000000";
            this.regNumOrgBox.Name = "regNumOrgBox";
            this.regNumOrgBox.Size = new System.Drawing.Size(90, 20);
            this.regNumOrgBox.TabIndex = 5;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(12, 114);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(45, 13);
            this.passwordLabel.TabIndex = 4;
            this.passwordLabel.Text = "Пароль";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(151, 111);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(193, 20);
            this.passwordTextBox.TabIndex = 10;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // showPassButton
            // 
            this.showPassButton.BackColor = System.Drawing.Color.Transparent;
            this.showPassButton.FlatAppearance.BorderSize = 0;
            this.showPassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showPassButton.Location = new System.Drawing.Point(352, 109);
            this.showPassButton.Name = "showPassButton";
            this.showPassButton.Size = new System.Drawing.Size(23, 23);
            this.showPassButton.TabIndex = 11;
            this.showPassButton.Text = "V";
            this.showPassButton.UseVisualStyleBackColor = false;
            this.showPassButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showPassButton_MouseDown);
            this.showPassButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.showPassButton_MouseUp);
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(12, 163);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(26, 13);
            this.logLabel.TabIndex = 16;
            this.logLabel.Text = "Лог";
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logRichTextBox.Location = new System.Drawing.Point(12, 179);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.Size = new System.Drawing.Size(363, 151);
            this.logRichTextBox.TabIndex = 17;
            this.logRichTextBox.Text = "";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(289, 137);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 23);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // InvokerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 342);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.logRichTextBox);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.showPassButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.regNumOrgBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.edataBrowseButton);
            this.Controls.Add(this.reportTypeComboBox);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.yearBox);
            this.Controls.Add(this.lanSettingsButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.reportTypeLabel);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.repYearLabel);
            this.Controls.Add(this.regNumLabel);
            this.Name = "InvokerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отправка данных на хостинг фонда";
            this.Load += new System.EventHandler(this.InvokerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.yearBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label regNumLabel;
        private System.Windows.Forms.Label repYearLabel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label reportTypeLabel;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button lanSettingsButton;
        private System.Windows.Forms.NumericUpDown yearBox;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.ComboBox reportTypeComboBox;
        private System.Windows.Forms.Button edataBrowseButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox regNumOrgBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button showPassButton;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.Button cancelButton;
    }
}