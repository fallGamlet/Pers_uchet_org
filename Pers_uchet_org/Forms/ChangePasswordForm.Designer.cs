namespace Pers_uchet_org.Forms
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
            this.oldPasswordBox = new System.Windows.Forms.TextBox();
            this.confPasswordBox = new System.Windows.Forms.TextBox();
            this.oldpasswordLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.showPassButton1 = new System.Windows.Forms.Button();
            this.showPassButton2 = new System.Windows.Forms.Button();
            this.showPassButton3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // canselButton
            // 
            this.canselButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.canselButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canselButton.Location = new System.Drawing.Point(281, 189);
            this.canselButton.Name = "canselButton";
            this.canselButton.Size = new System.Drawing.Size(75, 23);
            this.canselButton.TabIndex = 11;
            this.canselButton.Text = "Отмена";
            this.canselButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 98);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordBox.Location = new System.Drawing.Point(153, 136);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(174, 20);
            this.passwordBox.TabIndex = 6;
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            this.passwordBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passwordBox_KeyDown);
            // 
            // oldPasswordBox
            // 
            this.oldPasswordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oldPasswordBox.Location = new System.Drawing.Point(153, 110);
            this.oldPasswordBox.Name = "oldPasswordBox";
            this.oldPasswordBox.Size = new System.Drawing.Size(174, 20);
            this.oldPasswordBox.TabIndex = 4;
            this.oldPasswordBox.UseSystemPasswordChar = true;
            this.oldPasswordBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oldPasswordBox_KeyDown);
            // 
            // confPasswordBox
            // 
            this.confPasswordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.confPasswordBox.Location = new System.Drawing.Point(153, 162);
            this.confPasswordBox.Name = "confPasswordBox";
            this.confPasswordBox.Size = new System.Drawing.Size(174, 20);
            this.confPasswordBox.TabIndex = 8;
            this.confPasswordBox.UseSystemPasswordChar = true;
            this.confPasswordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            this.confPasswordBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.confPasswordBox_KeyDown);
            // 
            // oldpasswordLabel
            // 
            this.oldpasswordLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.oldpasswordLabel.AutoSize = true;
            this.oldpasswordLabel.Location = new System.Drawing.Point(12, 113);
            this.oldpasswordLabel.Name = "oldpasswordLabel";
            this.oldpasswordLabel.Size = new System.Drawing.Size(84, 13);
            this.oldpasswordLabel.TabIndex = 1;
            this.oldpasswordLabel.Text = "Старый пароль";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Новый пароль";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Подтверждение пароля";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(200, 189);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "Сохранить";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // showPassButton1
            // 
            this.showPassButton1.FlatAppearance.BorderSize = 0;
            this.showPassButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showPassButton1.Image = ((System.Drawing.Image)(resources.GetObject("showPassButton1.Image")));
            this.showPassButton1.Location = new System.Drawing.Point(333, 108);
            this.showPassButton1.Name = "showPassButton1";
            this.showPassButton1.Size = new System.Drawing.Size(23, 23);
            this.showPassButton1.TabIndex = 5;
            this.showPassButton1.UseVisualStyleBackColor = true;
            this.showPassButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showPassButton1_MouseDown);
            this.showPassButton1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.showPassButton1_MouseUp);
            // 
            // showPassButton2
            // 
            this.showPassButton2.FlatAppearance.BorderSize = 0;
            this.showPassButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showPassButton2.Image = ((System.Drawing.Image)(resources.GetObject("showPassButton2.Image")));
            this.showPassButton2.Location = new System.Drawing.Point(333, 134);
            this.showPassButton2.Name = "showPassButton2";
            this.showPassButton2.Size = new System.Drawing.Size(23, 23);
            this.showPassButton2.TabIndex = 7;
            this.showPassButton2.UseVisualStyleBackColor = true;
            this.showPassButton2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showPassButton2_MouseDown);
            this.showPassButton2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.showPassButton2_MouseUp);
            // 
            // showPassButton3
            // 
            this.showPassButton3.FlatAppearance.BorderSize = 0;
            this.showPassButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showPassButton3.Image = ((System.Drawing.Image)(resources.GetObject("showPassButton3.Image")));
            this.showPassButton3.Location = new System.Drawing.Point(333, 160);
            this.showPassButton3.Name = "showPassButton3";
            this.showPassButton3.Size = new System.Drawing.Size(23, 23);
            this.showPassButton3.TabIndex = 9;
            this.showPassButton3.UseVisualStyleBackColor = true;
            this.showPassButton3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showPassButton3_MouseDown);
            this.showPassButton3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.showPassButton3_MouseUp);
            // 
            // ChangePasswordForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.canselButton;
            this.ClientSize = new System.Drawing.Size(368, 224);
            this.Controls.Add(this.showPassButton3);
            this.Controls.Add(this.showPassButton2);
            this.Controls.Add(this.showPassButton1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.oldpasswordLabel);
            this.Controls.Add(this.confPasswordBox);
            this.Controls.Add(this.oldPasswordBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.canselButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание\\изменение пароля";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangePasswordForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button canselButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox oldPasswordBox;
        private System.Windows.Forms.TextBox confPasswordBox;
        private System.Windows.Forms.Label oldpasswordLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button showPassButton1;
        private System.Windows.Forms.Button showPassButton2;
        private System.Windows.Forms.Button showPassButton3;
    }
}