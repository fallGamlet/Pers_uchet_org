namespace Pers_uchet_org.Forms
{
    partial class SettingsLanForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.autoProxyRadioButton = new System.Windows.Forms.RadioButton();
            this.manualProxyRadioButton = new System.Windows.Forms.RadioButton();
            this.serverProxyLabel = new System.Windows.Forms.Label();
            this.serverProxyTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.portProxyTextBox = new System.Windows.Forms.TextBox();
            this.customCredentialsCheckBox = new System.Windows.Forms.CheckBox();
            this.loginProxyLabel = new System.Windows.Forms.Label();
            this.passwordProxyLabel = new System.Windows.Forms.Label();
            this.loginProxyTextBox = new System.Windows.Forms.TextBox();
            this.passwordProxyTextBox = new System.Windows.Forms.TextBox();
            this.showPassProxyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(222, 165);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(141, 165);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // autoProxyRadioButton
            // 
            this.autoProxyRadioButton.AutoSize = true;
            this.autoProxyRadioButton.Location = new System.Drawing.Point(64, 12);
            this.autoProxyRadioButton.Name = "autoProxyRadioButton";
            this.autoProxyRadioButton.Size = new System.Drawing.Size(166, 17);
            this.autoProxyRadioButton.TabIndex = 0;
            this.autoProxyRadioButton.TabStop = true;
            this.autoProxyRadioButton.Text = "Определять автоматически";
            this.autoProxyRadioButton.UseVisualStyleBackColor = true;
            this.autoProxyRadioButton.CheckedChanged += new System.EventHandler(this.autoRadioButton_CheckedChanged);
            // 
            // manualProxyRadioButton
            // 
            this.manualProxyRadioButton.AutoSize = true;
            this.manualProxyRadioButton.Location = new System.Drawing.Point(64, 35);
            this.manualProxyRadioButton.Name = "manualProxyRadioButton";
            this.manualProxyRadioButton.Size = new System.Drawing.Size(67, 17);
            this.manualProxyRadioButton.TabIndex = 1;
            this.manualProxyRadioButton.TabStop = true;
            this.manualProxyRadioButton.Text = "Вручную";
            this.manualProxyRadioButton.UseVisualStyleBackColor = true;
            // 
            // serverProxyLabel
            // 
            this.serverProxyLabel.AutoSize = true;
            this.serverProxyLabel.Location = new System.Drawing.Point(11, 61);
            this.serverProxyLabel.Name = "serverProxyLabel";
            this.serverProxyLabel.Size = new System.Drawing.Size(44, 13);
            this.serverProxyLabel.TabIndex = 2;
            this.serverProxyLabel.Text = "Сервер";
            // 
            // serverProxyTextBox
            // 
            this.serverProxyTextBox.Location = new System.Drawing.Point(64, 58);
            this.serverProxyTextBox.Name = "serverProxyTextBox";
            this.serverProxyTextBox.Size = new System.Drawing.Size(166, 20);
            this.serverProxyTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = ":";
            // 
            // portProxyTextBox
            // 
            this.portProxyTextBox.Location = new System.Drawing.Point(244, 58);
            this.portProxyTextBox.Name = "portProxyTextBox";
            this.portProxyTextBox.Size = new System.Drawing.Size(48, 20);
            this.portProxyTextBox.TabIndex = 5;
            // 
            // customCredentialsCheckBox
            // 
            this.customCredentialsCheckBox.AutoSize = true;
            this.customCredentialsCheckBox.Location = new System.Drawing.Point(64, 84);
            this.customCredentialsCheckBox.Name = "customCredentialsCheckBox";
            this.customCredentialsCheckBox.Size = new System.Drawing.Size(181, 17);
            this.customCredentialsCheckBox.TabIndex = 6;
            this.customCredentialsCheckBox.Text = "Использовать учетную запись";
            this.customCredentialsCheckBox.UseVisualStyleBackColor = true;
            this.customCredentialsCheckBox.CheckedChanged += new System.EventHandler(this.customCredentialsCheckBox_CheckedChanged);
            this.customCredentialsCheckBox.EnabledChanged += new System.EventHandler(this.customCredentialsCheckBox_EnabledChanged);
            // 
            // loginProxyLabel
            // 
            this.loginProxyLabel.AutoSize = true;
            this.loginProxyLabel.Location = new System.Drawing.Point(17, 110);
            this.loginProxyLabel.Name = "loginProxyLabel";
            this.loginProxyLabel.Size = new System.Drawing.Size(38, 13);
            this.loginProxyLabel.TabIndex = 7;
            this.loginProxyLabel.Text = "Логин";
            // 
            // passwordProxyLabel
            // 
            this.passwordProxyLabel.AutoSize = true;
            this.passwordProxyLabel.Location = new System.Drawing.Point(10, 136);
            this.passwordProxyLabel.Name = "passwordProxyLabel";
            this.passwordProxyLabel.Size = new System.Drawing.Size(45, 13);
            this.passwordProxyLabel.TabIndex = 9;
            this.passwordProxyLabel.Text = "Пароль";
            // 
            // loginProxyTextBox
            // 
            this.loginProxyTextBox.Location = new System.Drawing.Point(64, 107);
            this.loginProxyTextBox.Name = "loginProxyTextBox";
            this.loginProxyTextBox.Size = new System.Drawing.Size(228, 20);
            this.loginProxyTextBox.TabIndex = 8;
            // 
            // passwordProxyTextBox
            // 
            this.passwordProxyTextBox.Location = new System.Drawing.Point(64, 133);
            this.passwordProxyTextBox.Name = "passwordProxyTextBox";
            this.passwordProxyTextBox.Size = new System.Drawing.Size(199, 20);
            this.passwordProxyTextBox.TabIndex = 10;
            this.passwordProxyTextBox.UseSystemPasswordChar = true;
            // 
            // showPassProxyButton
            // 
            this.showPassProxyButton.BackColor = System.Drawing.Color.Transparent;
            this.showPassProxyButton.FlatAppearance.BorderSize = 0;
            this.showPassProxyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showPassProxyButton.Image = global::Pers_uchet_org.Properties.Resources.visible_16;
            this.showPassProxyButton.Location = new System.Drawing.Point(269, 131);
            this.showPassProxyButton.Name = "showPassProxyButton";
            this.showPassProxyButton.Size = new System.Drawing.Size(23, 23);
            this.showPassProxyButton.TabIndex = 11;
            this.showPassProxyButton.UseVisualStyleBackColor = false;
            this.showPassProxyButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showPassButton_MouseDown);
            this.showPassProxyButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.showPassButton_MouseUp);
            // 
            // SettingsLanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 200);
            this.Controls.Add(this.showPassProxyButton);
            this.Controls.Add(this.passwordProxyTextBox);
            this.Controls.Add(this.loginProxyTextBox);
            this.Controls.Add(this.passwordProxyLabel);
            this.Controls.Add(this.loginProxyLabel);
            this.Controls.Add(this.customCredentialsCheckBox);
            this.Controls.Add(this.portProxyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverProxyTextBox);
            this.Controls.Add(this.serverProxyLabel);
            this.Controls.Add(this.manualProxyRadioButton);
            this.Controls.Add(this.autoProxyRadioButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.MaximizeBox = false;
            this.Name = "SettingsLanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Параметры прокси-сервера";
            this.Load += new System.EventHandler(this.SettingsLanForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.RadioButton autoProxyRadioButton;
        private System.Windows.Forms.RadioButton manualProxyRadioButton;
        private System.Windows.Forms.Label serverProxyLabel;
        private System.Windows.Forms.TextBox serverProxyTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox portProxyTextBox;
        private System.Windows.Forms.CheckBox customCredentialsCheckBox;
        private System.Windows.Forms.Label loginProxyLabel;
        private System.Windows.Forms.Label passwordProxyLabel;
        private System.Windows.Forms.TextBox loginProxyTextBox;
        private System.Windows.Forms.TextBox passwordProxyTextBox;
        private System.Windows.Forms.Button showPassProxyButton;

    }
}