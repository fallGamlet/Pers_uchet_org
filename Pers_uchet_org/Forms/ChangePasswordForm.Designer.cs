namespace Pers_uchet_org
{
    partial class ChangePasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePasswordForm));
            this.canselButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.oldpasswordBox = new System.Windows.Forms.TextBox();
            this.confpasswordBox = new System.Windows.Forms.TextBox();
            this.oldpasswordLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // canselButton
            // 
            this.canselButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.canselButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canselButton.Location = new System.Drawing.Point(273, 243);
            this.canselButton.Name = "canselButton";
            this.canselButton.Size = new System.Drawing.Size(75, 23);
            this.canselButton.TabIndex = 5;
            this.canselButton.Text = "Отмена";
            this.canselButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 125);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordBox.Location = new System.Drawing.Point(145, 159);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(203, 20);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // oldpasswordBox
            // 
            this.oldpasswordBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.oldpasswordBox.Location = new System.Drawing.Point(145, 133);
            this.oldpasswordBox.Name = "oldpasswordBox";
            this.oldpasswordBox.PasswordChar = '*';
            this.oldpasswordBox.Size = new System.Drawing.Size(203, 20);
            this.oldpasswordBox.TabIndex = 1;
            this.oldpasswordBox.UseSystemPasswordChar = true;
            this.oldpasswordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // confpasswordBox
            // 
            this.confpasswordBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confpasswordBox.Location = new System.Drawing.Point(145, 185);
            this.confpasswordBox.Name = "confpasswordBox";
            this.confpasswordBox.Size = new System.Drawing.Size(203, 20);
            this.confpasswordBox.TabIndex = 3;
            this.confpasswordBox.UseSystemPasswordChar = true;
            this.confpasswordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // oldpasswordLabel
            // 
            this.oldpasswordLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.oldpasswordLabel.AutoSize = true;
            this.oldpasswordLabel.Location = new System.Drawing.Point(12, 136);
            this.oldpasswordLabel.Name = "oldpasswordLabel";
            this.oldpasswordLabel.Size = new System.Drawing.Size(84, 13);
            this.oldpasswordLabel.TabIndex = 6;
            this.oldpasswordLabel.Text = "Старый пароль";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Новый пароль";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Подтверждение пароля";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(192, 243);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Принять";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // ChangePasswordForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.canselButton;
            this.ClientSize = new System.Drawing.Size(360, 278);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.oldpasswordLabel);
            this.Controls.Add(this.confpasswordBox);
            this.Controls.Add(this.oldpasswordBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.canselButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.Text = "Смена пароля";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangePasswordForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button canselButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox oldpasswordBox;
        private System.Windows.Forms.TextBox confpasswordBox;
        private System.Windows.Forms.Label oldpasswordLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okButton;
    }
}