namespace Pers_uchet_org.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generalPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.showPassProxyButton = new System.Windows.Forms.Button();
            this.passwordProxyTextBox = new System.Windows.Forms.TextBox();
            this.loginProxyTextBox = new System.Windows.Forms.TextBox();
            this.passwordProxyLabel = new System.Windows.Forms.Label();
            this.loginProxyLabel = new System.Windows.Forms.Label();
            this.customCredentialsCheckBox = new System.Windows.Forms.CheckBox();
            this.portProxyTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.serverProxyTextBox = new System.Windows.Forms.TextBox();
            this.serverProxyLabel = new System.Windows.Forms.Label();
            this.manualProxyRadioButton = new System.Windows.Forms.RadioButton();
            this.autoProxyRadioButton = new System.Windows.Forms.RadioButton();
            this.backupGroupBox = new System.Windows.Forms.GroupBox();
            this.createBackupButton = new System.Windows.Forms.Button();
            this.backupBrowseButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.backupPathTextBox = new System.Windows.Forms.TextBox();
            this.backupMaxCountBox = new System.Windows.Forms.NumericUpDown();
            this.isBackupEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.otherPage = new System.Windows.Forms.TabPage();
            this.databaseGroupBox = new System.Windows.Forms.GroupBox();
            this.vacuumButton = new System.Windows.Forms.Button();
            this.databaseBrowseButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.databasePathTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.generalPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.backupGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backupMaxCountBox)).BeginInit();
            this.otherPage.SuspendLayout();
            this.databaseGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.generalPage);
            this.tabControl1.Controls.Add(this.otherPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(532, 363);
            this.tabControl1.TabIndex = 0;
            // 
            // generalPage
            // 
            this.generalPage.Controls.Add(this.groupBox1);
            this.generalPage.Controls.Add(this.backupGroupBox);
            this.generalPage.Location = new System.Drawing.Point(4, 22);
            this.generalPage.Name = "generalPage";
            this.generalPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalPage.Size = new System.Drawing.Size(524, 337);
            this.generalPage.TabIndex = 0;
            this.generalPage.Text = "Основные";
            this.generalPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.showPassProxyButton);
            this.groupBox1.Controls.Add(this.passwordProxyTextBox);
            this.groupBox1.Controls.Add(this.loginProxyTextBox);
            this.groupBox1.Controls.Add(this.passwordProxyLabel);
            this.groupBox1.Controls.Add(this.loginProxyLabel);
            this.groupBox1.Controls.Add(this.customCredentialsCheckBox);
            this.groupBox1.Controls.Add(this.portProxyTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.serverProxyTextBox);
            this.groupBox1.Controls.Add(this.serverProxyLabel);
            this.groupBox1.Controls.Add(this.manualProxyRadioButton);
            this.groupBox1.Controls.Add(this.autoProxyRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(8, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 173);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Прокси-сервер";
            // 
            // showPassProxyButton
            // 
            this.showPassProxyButton.BackColor = System.Drawing.Color.Transparent;
            this.showPassProxyButton.FlatAppearance.BorderSize = 0;
            this.showPassProxyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showPassProxyButton.Image = global::Pers_uchet_org.Properties.Resources.visible_16;
            this.showPassProxyButton.Location = new System.Drawing.Point(304, 138);
            this.showPassProxyButton.Name = "showPassProxyButton";
            this.showPassProxyButton.Size = new System.Drawing.Size(23, 23);
            this.showPassProxyButton.TabIndex = 23;
            this.showPassProxyButton.UseVisualStyleBackColor = false;
            this.showPassProxyButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showPassProxyButton_MouseDown);
            this.showPassProxyButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.showPassProxyButton_MouseUp);
            // 
            // passwordProxyTextBox
            // 
            this.passwordProxyTextBox.Location = new System.Drawing.Point(65, 140);
            this.passwordProxyTextBox.Name = "passwordProxyTextBox";
            this.passwordProxyTextBox.Size = new System.Drawing.Size(233, 20);
            this.passwordProxyTextBox.TabIndex = 22;
            this.passwordProxyTextBox.UseSystemPasswordChar = true;
            // 
            // loginProxyTextBox
            // 
            this.loginProxyTextBox.Location = new System.Drawing.Point(65, 114);
            this.loginProxyTextBox.Name = "loginProxyTextBox";
            this.loginProxyTextBox.Size = new System.Drawing.Size(262, 20);
            this.loginProxyTextBox.TabIndex = 20;
            // 
            // passwordProxyLabel
            // 
            this.passwordProxyLabel.AutoSize = true;
            this.passwordProxyLabel.Location = new System.Drawing.Point(11, 143);
            this.passwordProxyLabel.Name = "passwordProxyLabel";
            this.passwordProxyLabel.Size = new System.Drawing.Size(45, 13);
            this.passwordProxyLabel.TabIndex = 21;
            this.passwordProxyLabel.Text = "Пароль";
            // 
            // loginProxyLabel
            // 
            this.loginProxyLabel.AutoSize = true;
            this.loginProxyLabel.Location = new System.Drawing.Point(18, 117);
            this.loginProxyLabel.Name = "loginProxyLabel";
            this.loginProxyLabel.Size = new System.Drawing.Size(38, 13);
            this.loginProxyLabel.TabIndex = 19;
            this.loginProxyLabel.Text = "Логин";
            // 
            // customCredentialsCheckBox
            // 
            this.customCredentialsCheckBox.AutoSize = true;
            this.customCredentialsCheckBox.Location = new System.Drawing.Point(65, 91);
            this.customCredentialsCheckBox.Name = "customCredentialsCheckBox";
            this.customCredentialsCheckBox.Size = new System.Drawing.Size(181, 17);
            this.customCredentialsCheckBox.TabIndex = 18;
            this.customCredentialsCheckBox.Text = "Использовать учетную запись";
            this.customCredentialsCheckBox.UseVisualStyleBackColor = true;
            this.customCredentialsCheckBox.CheckedChanged += new System.EventHandler(this.customCredentialsCheckBox_CheckedChanged);
            this.customCredentialsCheckBox.EnabledChanged += new System.EventHandler(this.customCredentialsCheckBox_EnabledChanged);
            // 
            // portProxyTextBox
            // 
            this.portProxyTextBox.Location = new System.Drawing.Point(279, 65);
            this.portProxyTextBox.Name = "portProxyTextBox";
            this.portProxyTextBox.Size = new System.Drawing.Size(48, 20);
            this.portProxyTextBox.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = ":";
            // 
            // serverProxyTextBox
            // 
            this.serverProxyTextBox.Location = new System.Drawing.Point(65, 65);
            this.serverProxyTextBox.Name = "serverProxyTextBox";
            this.serverProxyTextBox.Size = new System.Drawing.Size(199, 20);
            this.serverProxyTextBox.TabIndex = 15;
            // 
            // serverProxyLabel
            // 
            this.serverProxyLabel.AutoSize = true;
            this.serverProxyLabel.Location = new System.Drawing.Point(12, 68);
            this.serverProxyLabel.Name = "serverProxyLabel";
            this.serverProxyLabel.Size = new System.Drawing.Size(44, 13);
            this.serverProxyLabel.TabIndex = 14;
            this.serverProxyLabel.Text = "Сервер";
            // 
            // manualProxyRadioButton
            // 
            this.manualProxyRadioButton.AutoSize = true;
            this.manualProxyRadioButton.Location = new System.Drawing.Point(65, 42);
            this.manualProxyRadioButton.Name = "manualProxyRadioButton";
            this.manualProxyRadioButton.Size = new System.Drawing.Size(67, 17);
            this.manualProxyRadioButton.TabIndex = 13;
            this.manualProxyRadioButton.TabStop = true;
            this.manualProxyRadioButton.Text = "Вручную";
            this.manualProxyRadioButton.UseVisualStyleBackColor = true;
            // 
            // autoProxyRadioButton
            // 
            this.autoProxyRadioButton.AutoSize = true;
            this.autoProxyRadioButton.Location = new System.Drawing.Point(65, 19);
            this.autoProxyRadioButton.Name = "autoProxyRadioButton";
            this.autoProxyRadioButton.Size = new System.Drawing.Size(166, 17);
            this.autoProxyRadioButton.TabIndex = 12;
            this.autoProxyRadioButton.TabStop = true;
            this.autoProxyRadioButton.Text = "Определять автоматически";
            this.autoProxyRadioButton.UseVisualStyleBackColor = true;
            this.autoProxyRadioButton.CheckedChanged += new System.EventHandler(this.autoProxyRadioButton_CheckedChanged);
            // 
            // backupGroupBox
            // 
            this.backupGroupBox.Controls.Add(this.createBackupButton);
            this.backupGroupBox.Controls.Add(this.backupBrowseButton);
            this.backupGroupBox.Controls.Add(this.label11);
            this.backupGroupBox.Controls.Add(this.backupPathTextBox);
            this.backupGroupBox.Controls.Add(this.backupMaxCountBox);
            this.backupGroupBox.Controls.Add(this.isBackupEnableCheckBox);
            this.backupGroupBox.Controls.Add(this.label1);
            this.backupGroupBox.Location = new System.Drawing.Point(8, 6);
            this.backupGroupBox.Name = "backupGroupBox";
            this.backupGroupBox.Size = new System.Drawing.Size(340, 147);
            this.backupGroupBox.TabIndex = 0;
            this.backupGroupBox.TabStop = false;
            this.backupGroupBox.Text = "Резервное копирование";
            // 
            // createBackupButton
            // 
            this.createBackupButton.Location = new System.Drawing.Point(27, 108);
            this.createBackupButton.Name = "createBackupButton";
            this.createBackupButton.Size = new System.Drawing.Size(247, 23);
            this.createBackupButton.TabIndex = 6;
            this.createBackupButton.Text = "Создать резервную копию сейчас";
            this.createBackupButton.UseVisualStyleBackColor = true;
            this.createBackupButton.Click += new System.EventHandler(this.createBackupButton_Click);
            // 
            // backupBrowseButton
            // 
            this.backupBrowseButton.Location = new System.Drawing.Point(280, 80);
            this.backupBrowseButton.Name = "backupBrowseButton";
            this.backupBrowseButton.Size = new System.Drawing.Size(47, 23);
            this.backupBrowseButton.TabIndex = 5;
            this.backupBrowseButton.Text = "...";
            this.backupBrowseButton.UseVisualStyleBackColor = true;
            this.backupBrowseButton.Click += new System.EventHandler(this.backupBrowseButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Папка резервных копий";
            // 
            // backupPathTextBox
            // 
            this.backupPathTextBox.Location = new System.Drawing.Point(27, 82);
            this.backupPathTextBox.Name = "backupPathTextBox";
            this.backupPathTextBox.Size = new System.Drawing.Size(247, 20);
            this.backupPathTextBox.TabIndex = 4;
            // 
            // backupMaxCountBox
            // 
            this.backupMaxCountBox.Location = new System.Drawing.Point(237, 42);
            this.backupMaxCountBox.Name = "backupMaxCountBox";
            this.backupMaxCountBox.Size = new System.Drawing.Size(90, 20);
            this.backupMaxCountBox.TabIndex = 2;
            this.backupMaxCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // isBackupEnableCheckBox
            // 
            this.isBackupEnableCheckBox.AutoSize = true;
            this.isBackupEnableCheckBox.Location = new System.Drawing.Point(6, 19);
            this.isBackupEnableCheckBox.Name = "isBackupEnableCheckBox";
            this.isBackupEnableCheckBox.Size = new System.Drawing.Size(310, 17);
            this.isBackupEnableCheckBox.TabIndex = 0;
            this.isBackupEnableCheckBox.Text = "Создавать резервные копии при выходе из программы";
            this.isBackupEnableCheckBox.UseVisualStyleBackColor = true;
            this.isBackupEnableCheckBox.CheckedChanged += new System.EventHandler(this.isBackupEnableCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Максимальное число резервных копий";
            // 
            // otherPage
            // 
            this.otherPage.Controls.Add(this.databaseGroupBox);
            this.otherPage.Location = new System.Drawing.Point(4, 22);
            this.otherPage.Name = "otherPage";
            this.otherPage.Padding = new System.Windows.Forms.Padding(3);
            this.otherPage.Size = new System.Drawing.Size(524, 337);
            this.otherPage.TabIndex = 1;
            this.otherPage.Text = "Дополнительные";
            this.otherPage.UseVisualStyleBackColor = true;
            // 
            // databaseGroupBox
            // 
            this.databaseGroupBox.Controls.Add(this.vacuumButton);
            this.databaseGroupBox.Controls.Add(this.databaseBrowseButton);
            this.databaseGroupBox.Controls.Add(this.label3);
            this.databaseGroupBox.Controls.Add(this.databasePathTextBox);
            this.databaseGroupBox.Location = new System.Drawing.Point(8, 6);
            this.databaseGroupBox.Name = "databaseGroupBox";
            this.databaseGroupBox.Size = new System.Drawing.Size(330, 135);
            this.databaseGroupBox.TabIndex = 0;
            this.databaseGroupBox.TabStop = false;
            this.databaseGroupBox.Text = "База данных";
            // 
            // vacuumButton
            // 
            this.vacuumButton.Location = new System.Drawing.Point(9, 58);
            this.vacuumButton.Name = "vacuumButton";
            this.vacuumButton.Size = new System.Drawing.Size(195, 23);
            this.vacuumButton.TabIndex = 3;
            this.vacuumButton.Text = "Сжать базу данных (VACUUM)";
            this.vacuumButton.UseVisualStyleBackColor = true;
            this.vacuumButton.Click += new System.EventHandler(this.vacuumButton_Click);
            // 
            // databaseBrowseButton
            // 
            this.databaseBrowseButton.Location = new System.Drawing.Point(274, 30);
            this.databaseBrowseButton.Name = "databaseBrowseButton";
            this.databaseBrowseButton.Size = new System.Drawing.Size(47, 23);
            this.databaseBrowseButton.TabIndex = 2;
            this.databaseBrowseButton.Text = "...";
            this.databaseBrowseButton.UseVisualStyleBackColor = true;
            this.databaseBrowseButton.Click += new System.EventHandler(this.databaseBrowseButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Папка базы данных";
            // 
            // databasePathTextBox
            // 
            this.databasePathTextBox.Location = new System.Drawing.Point(9, 32);
            this.databasePathTextBox.Name = "databasePathTextBox";
            this.databasePathTextBox.Size = new System.Drawing.Size(259, 20);
            this.databasePathTextBox.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(362, 375);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(443, 375);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 410);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки программы";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.generalPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.backupGroupBox.ResumeLayout(false);
            this.backupGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backupMaxCountBox)).EndInit();
            this.otherPage.ResumeLayout(false);
            this.databaseGroupBox.ResumeLayout(false);
            this.databaseGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage generalPage;
        private System.Windows.Forms.TabPage otherPage;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox backupGroupBox;
        private System.Windows.Forms.Button backupBrowseButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox backupPathTextBox;
        private System.Windows.Forms.NumericUpDown backupMaxCountBox;
        private System.Windows.Forms.CheckBox isBackupEnableCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox databaseGroupBox;
        private System.Windows.Forms.Button vacuumButton;
        private System.Windows.Forms.Button databaseBrowseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox databasePathTextBox;
        private System.Windows.Forms.Button createBackupButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button showPassProxyButton;
        private System.Windows.Forms.TextBox passwordProxyTextBox;
        private System.Windows.Forms.TextBox loginProxyTextBox;
        private System.Windows.Forms.Label passwordProxyLabel;
        private System.Windows.Forms.Label loginProxyLabel;
        private System.Windows.Forms.CheckBox customCredentialsCheckBox;
        private System.Windows.Forms.TextBox portProxyTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox serverProxyTextBox;
        private System.Windows.Forms.Label serverProxyLabel;
        private System.Windows.Forms.RadioButton manualProxyRadioButton;
        private System.Windows.Forms.RadioButton autoProxyRadioButton;
        //private orgDBDataSetTableAdapters.OrgTableAdapter orgTableAdapter1;
    }
}